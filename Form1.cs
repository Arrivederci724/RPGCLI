using System.Drawing;

namespace HttpAndAPI;
using System;
using System.Windows.Forms;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text;

public partial class Form1 : Form
{
    private static HttpClient http = new();
    private RichTextBox MessageUp;
    private string textGet = "";
    private string GetData = "";
    private Button Jstost = new();
    TextBox text = new TextBox();
    public async Task UpdateHTTP()
    {
      try
      {
            var Data = await http.GetAsync(textGet);
            GetData = await Data.Content.ReadAsStringAsync();
       }
       catch(Exception e)
       {
          MessageUp.Text = Convert.ToString(e);
       }
    }
    
    public Form1()
    {
        InitializeComponent();
        this.Text = "HttpAndAPI";
        
        text.Size = new Size(520, 30);
        text.Text = "Enter URL";
        Controls.Add(text);
        text.KeyPress += textEnter;
        text.KeyDown += textCtrl;

        MessageUp = new RichTextBox();
        MessageUp.ReadOnly =  true;
        MessageUp.WordWrap = true;
        MessageUp.Location = new Point(0,35);
        MessageUp.Size = new Size(800,600);
        Controls.Add(MessageUp);
    
        Jstost.Location = new Point(525,0);
        Jstost.Text = "序列化";
        Jstost.Click += new EventHandler(JJon);
        Controls.Add(Jstost);
    }
    public async void textEnter(object? sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)13)
        {
            textGet = text.Text;
            MessageUp.AppendText($"\n!Not Html Content:::Successful:{textGet}");
        }
    }

    public async void textCtrl(object? sender, KeyEventArgs e)
    {
        if (e.Control)
        {
            await UpdateHTTP();
            MessageUp.Text = GetData;
        }
    }
    public void JJon(object? sender,EventArgs e){
      try{
            string J = JsonSerializer.Serialize(MessageUp.Text);
            MessageUp.Text = J;
      }
      catch(Exception ex){
        MessageUp.Text = $"此信息不能被序列化 请重试 或查看具体错误\n\n{ex}";
      }
    }
}
