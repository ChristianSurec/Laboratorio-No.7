namespace Laboratorio_No._7
{
    public partial class Form1 : Form
    {
        List<Propietario> propietarios = new List<Propietario>();
        List<Propiedad> propiedades = new List<Propiedad>();
        List<Resumen> resumen = new List<Resumen>();    

        public Form1()
        {
            InitializeComponent();
        }

        private void GuardarPropietario()
        {
            FileStream stream = new FileStream("Propietario.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);

            foreach (var propietario in propietarios)
            {
                writer.WriteLine(propietario.dpi);
                writer.WriteLine(propietario.nombre);
                writer.WriteLine(propietario.apellido);               
            }           
            writer.Close();
        }

        private void GuardarPropiedad()
        {
            FileStream stream = new FileStream("Propiedades.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);

            foreach (var propiedad in propiedades)
            {
                writer.WriteLine(propiedad.numeroCasa);
                writer.WriteLine(propiedad.DPI);
                writer.WriteLine(propiedad.cuotaMantenimiento);
            }
            writer.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Propietario propietario = new Propietario();
            propietario.dpi = textBoxDPI.Text;
            propietario.nombre = textBoxNombre.Text;
            propietario.apellido = textBoxApellido.Text;

            propietarios.Add(propietario);
            GuardarPropietario();

        }

        private void MostrarDatos()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            dataGridView1.DataSource = resumen;
            dataGridView1.Refresh();
        }

        private void buttonCuotas_Click_1(object sender, EventArgs e)
        {
            Propiedad propiedad = new Propiedad();
            propiedad.numeroCasa = textBoxNoCasa.Text;
            propiedad.DPI = textBoxDpiDueño.Text;
            propiedad.cuotaMantenimiento = Convert.ToDecimal(textBoxMantenimiento.Text);

            propiedades.Add(propiedad);
            GuardarPropiedad();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        private void mostrarPropiedades()
        {
            FileStream stream = new FileStream("Propiedades.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);


            while (reader.Peek() > -1)

            {
                Propiedad propiedad = new Propiedad();
                propiedad.numeroCasa = reader.ReadLine();
                propiedad.DPI = reader.ReadLine();  
                propiedad.cuotaMantenimiento= Convert.ToDecimal(reader.ReadLine());

                propiedades.Add(propiedad);

            }

            reader.Close();
        }

        private void mostrarPropietarios()
        {
            FileStream stream = new FileStream("Propietario.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);


            while (reader.Peek() > -1)

            {
                Propietario propietario = new Propietario();
                propietario.dpi = reader.ReadLine();
                propietario.nombre = reader.ReadLine();
                propietario.apellido = reader.ReadLine();

                propietarios.Add(propietario);

            }

            reader.Close();
        }


        private void buttonMostrar_Click(object sender, EventArgs e)
        {
            mostrarPropiedades();
            mostrarPropietarios();

            for(int i = 0; i < propiedades.Count; i++)
            {
                for(int j = 0; j < propietarios.Count; j++)
                {
                    if(propiedades[i].DPI == propietarios[j].dpi)
                    {
                        Resumen datoResumen = new Resumen();
                        datoResumen.nombre = propietarios[j].nombre;
                        datoResumen.apellido = propietarios[j].apellido;
                        datoResumen.noCasa = propiedades[i].numeroCasa;
                        datoResumen.cuotaMantenimiento = propiedades[j].cuotaMantenimiento;
                        resumen.Add(datoResumen);

                    }
                }
            }

            MostrarDatos();
            buttonOrdenar.Enabled = true;  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            resumen = resumen.OrderBy(a => a.cuotaMantenimiento).ToList();
            MostrarDatos();
            buttonMayorMenor.Enabled = true;
        }

        private void buttonMayorMenor_Click(object sender, EventArgs e)
        {
            label8.Text = resumen[0].cuotaMantenimiento.ToString();

            int cuantos = resumen.Count();
            label7.Text = resumen[cuantos - 1].cuotaMantenimiento.ToString();
            labelNombre.Text = resumen[cuantos - 1].nombre + ", " + resumen[cuantos - 1].apellido;
        }
    }
        
}