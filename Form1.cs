using System.Drawing;
using System.Threading.Tasks;

namespace AIAPIAI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;

public class Message
{
    public required string role { get; set; }
    public required string content { get; set; }
}

public class ChatRequest
{
    public required string model { get; set; }
    public required Message[] messages { get; set; }
}

public class AppConfig
{
    public string? APIURL { get; set; }
    public string? APIKEY { get; set; }
    public string? MODEL { get; set; }
}

[JsonSerializable(typeof(ChatRequest))]
[JsonSerializable(typeof(AppConfig))]
public partial class AppJsonContext : JsonSerializerContext { }

public partial class Form1 : Form
{
    private HttpClient http = new();
    private List<Message> history = new();
    private TextBox APIKEY = new();
    private TextBox APIURL = new();
    private TextBox MODEL = new();
    private TextBox MESSAGE = new();
    private RichTextBox RichT = new();
    private static readonly string ConfigPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "AIAPIAI",
        "config.json"
    );

    public Form1()
    {
        InitializeComponent();

        APIKEY.Text = "APIKEY";
        APIURL.Text = "APIURL";
        MODEL.Text = "MODEL";
        MESSAGE.Text = "MESSAGE";

        APIKEY.Size = new Size(300, 35);
        APIURL.Size = new Size(300, 35);
        MODEL.Size = new Size(300, 35);
        MESSAGE.Size = new Size(500, 80);

        APIURL.Location = new Point(0, 40);
        MODEL.Location = new Point(0, 80);
        MESSAGE.Location = new Point(0, 120);

        RichT.Size = new Size(800, 800);
        RichT.Location = new Point(0, 160);
        RichT.ReadOnly = true;
        
        RichT.Font = new Font("Segoe UI Emoji", 10);

        Controls.Add(APIKEY);
        Controls.Add(APIURL);
        Controls.Add(MODEL);
        Controls.Add(MESSAGE);
        Controls.Add(RichT);

        MESSAGE.KeyPress += OnMessageKeyPress;

        history.Add(new Message { role = "system", content = "请始终使用中文回复,别用Emoji 看不着 一定不要用" });
        
        LoadConfig();
    }

    private void LoadConfig()
    {
        try
        {
            if (File.Exists(ConfigPath))
            {
                var json = File.ReadAllText(ConfigPath);
                var config = JsonSerializer.Deserialize(json, AppJsonContext.Default.AppConfig);
                if (config != null)
                {
                    if (!string.IsNullOrEmpty(config.APIURL))
                        APIURL.Text = config.APIURL;
                    if (!string.IsNullOrEmpty(config.APIKEY))
                        APIKEY.Text = config.APIKEY;
                    if (!string.IsNullOrEmpty(config.MODEL))
                        MODEL.Text = config.MODEL;
                }
            }
        }
        catch
        {
            // 加载失败时忽略，使用默认值
        }
    }

    private void SaveConfig()
    {
        try
        {
            var directory = Path.GetDirectoryName(ConfigPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var config = new AppConfig
            {
                APIURL = APIURL.Text,
                APIKEY = APIKEY.Text,
                MODEL = MODEL.Text
            };
            var json = JsonSerializer.Serialize(config, AppJsonContext.Default.AppConfig);
            File.WriteAllText(ConfigPath, json);
        }
        catch
        {
            // 保存失败时忽略
        }
    }

    public async void OnMessageKeyPress(object? sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)13)
        {
            string userMessage = MESSAGE.Text;
            if (string.IsNullOrWhiteSpace(userMessage)) return;

            RichT.AppendText($"User: {userMessage}\n");
            MESSAGE.Text = "";
            MESSAGE.Enabled = false;

            try
            {
                http.DefaultRequestHeaders.Clear();
                http.DefaultRequestHeaders.Add("Authorization", $"Bearer {APIKEY.Text}");

                history.Add(new Message { role = "user", content = userMessage });

                var config = new ChatRequest
                {
                    model = MODEL.Text,
                    messages = history.ToArray()
                };

                var json = JsonSerializer.Serialize(config, AppJsonContext.Default.ChatRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await http.PostAsync(APIURL.Text, content);
                var responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var doc = JsonDocument.Parse(responseBody);
                    var answer = doc.RootElement
                        .GetProperty("choices")[0]
                        .GetProperty("message")
                        .GetProperty("content")
                        .GetString();

                    RichT.AppendText("AI: ");
                    if (answer != null)
                    {
                        foreach (char c in answer)
                        {
                            RichT.AppendText(c.ToString());
                            //RichT.ScrollToCaret();
                            await Task.Delay(30);
                        }
                    }
                    RichT.AppendText("\n");

                    history.Add(new Message { role = "assistant", content = answer ?? "" });
                    
                    SaveConfig();
                }
                else
                {
                    RichT.AppendText($"AI: 请求失败 - {response.StatusCode}\n");
                }
            }
            catch (Exception ex)
            {
                RichT.AppendText($"AI: 发生错误 - {ex.Message}\n");
            }
            finally
            {
                MESSAGE.Enabled = true;
                MESSAGE.Focus();
            }
        }
    }
}
