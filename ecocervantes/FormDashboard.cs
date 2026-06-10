using System;
using System.Linq;
using System.Windows.Forms;
using MongoDB.Driver;
using System.Windows.Forms.DataVisualization.Charting;

public partial class FormDashboard : Form
{
    public FormDashboard()
    {
        InitializeComponent();
    }

    private void FormDashboard_Load(object sender, EventArgs e)
    {
        var db = MongoDBConexion.GetDB();
        var col = db.GetCollection<Indicador>("indicadores");

        var datos = col.Find(_ => true).ToList();

        chart1.Series.Clear();
        chart1.Series.Add("Indicadores");

        foreach (var d in datos)
        {
            chart1.Series["Indicadores"].Points.AddXY(d.Tipo, d.Valor);
        }
    }

    private void btnIA_Click(object sender, EventArgs e)
    {
        var db = MongoDBConexion.GetDB();
        var col = db.GetCollection<Indicador>("indicadores");

        var datos = col.Find(_ => true).ToList();

        var problema = datos.Any(d => d.Valor > 50);

        string resultado = problema
            ? "Sistema ineficiente"
            : "Sistema eficiente";

        MessageBox.Show(resultado);
    }
}