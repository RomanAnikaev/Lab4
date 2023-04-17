using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.IO;
using static Lab4Anikaev.Form1;
using System.Xml.Serialization;

namespace Lab4Anikaev
{
    public partial class Form1 : Form
    {
        public static Control form1;

        public static Random r = new Random();

        public int col_std = 0;//кількість студентів
        
        public static string[] names = { "Lilla", "Misa", "Soni",
      "Mellisa", "Zinaida", "Zinna", "Rita", "Fry", "Met", "Kris",
      "Kiril", "Dok", "Mike", "Jek", "Nikita", "Den", "Kir"};
        //Прізвище
        public static string[] surnames = { "Williams", "Evans", "Stone",
      "Roberts", "Mills", "Lewis", "Morgan", "Florence", "Campbell", "Bronte",
      "Bell", "Adams", "Peters", "Gibson", "Martin", "Jordan", "Grant", "Davis", "Collins", "Barlow" };
        //предмети
        public static string[] items = { "C#", "C++", "SoC", "Python", "VHDL" };

        List<Student> students = new List<Student>();

        [Serializable]
        public class Grade
        {
            public int grade;
            public string item;

            public Grade() { }
            public Grade(string items_1)
            {
                Grades = r.Next(3,6);
                Item = items_1;
            }
            public int Grades
            {
                get => this.grade;
                set => this.grade = value;
            }
            public string Item
            {
                get => this.item;
                set => this.item = value;
            }
        }
        [Serializable]
        public class Student
        {
            //імя, призвіще, по-батькові, факультет, стать, група
            public string name, surname;
           
            public List<Grade> grades = new List<Grade>();//Колекція оцінок студента

            public Student()//string name1, string surname
            {
                int temp;
                temp = r.Next(0,17);
                Name = names[temp];
                temp = r.Next(0, 20);
                Surname = surnames[temp];
                for(int i = 0; i < 5; i++)
                {
                    grades.Add(new Grade(items[i]) { });//un6 - тип кімнати
                }

            }

            public string Name
            {
                get => this.name;
                set => this.name = value;
            }
            public string Surname
            {
                get => this.surname;
                set => this.surname = value;
            }

        }
        public Form1()
        {
            InitializeComponent();
        }
        //Додати Студента
        private void button1_Click(object sender, EventArgs e)
        {
             
            students.Add(new Student() { });
            col_std += 1;// +1 student
            //students.Add(new Student() { });
            BindingSource binding = new BindingSource();
            binding.DataSource = students;
            dataGridView1.DataSource = binding;
        }
        //Сортувати студентів за призвіщем
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                students = students.OrderBy(nams => nams.Surname).ToList();
                BindingSource binding = new BindingSource();
                binding.DataSource = students;
                dataGridView1.DataSource = binding;
            }
            catch
            {
                MessageBox.Show("Помилка!", "Form Closing",
                               MessageBoxButtons.YesNo,
                              MessageBoxIcon.Question);
            }
        }
        //Вивести оцінки студента
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int temp = Convert.ToInt32(textBox3.Text);//Номер студента
                BindingSource binding = new BindingSource();
                binding.DataSource = students[temp - 1].grades;
                dataGridView2.DataSource = binding;
            }
            catch
            {
                MessageBox.Show("Немає студента під таким номером!", "Form Closing",
                               MessageBoxButtons.YesNo,
                              MessageBoxIcon.Question);
            }
        }
        //Вивести студентів за оцінкою та предметом
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                Form2 fm2 = new Form2();
                form1 = this;
                fm2.Show();

                int temp1 = Convert.ToInt32(textBox1.Text);//Оцінка
                string temp2 = textBox2.Text;//Предмет
                var temp3 = from student in students from grades1 in student.grades 
                            where (grades1.grade == temp1 && grades1.item == temp2) 
                            select student;

                //if (DialogResult.OK == fm2.DialogResult)
                //{
                BindingSource binding = new BindingSource();
                binding.DataSource = temp3;

                fm2.dataGridView1.DataSource = binding;
                //}
            }
            catch
            {
                MessageBox.Show("Невірно введені данні!", "Form Closing",
                               MessageBoxButtons.YesNo,
                              MessageBoxIcon.Question);
            }
        }
        //Видалити студента
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int temp = Convert.ToInt32(textBox4.Text);//Номер студента для видалення
                students.RemoveAt(temp-1);
                col_std -= 1; //-1 студент
                BindingSource binding = new BindingSource();
                binding.DataSource = students;
                dataGridView1.DataSource = binding;
            }
            catch
            {
                MessageBox.Show("Немає такого студента!", "Form Closing",
                               MessageBoxButtons.YesNo,
                              MessageBoxIcon.Question);
            }

        }
        //Змінити Імя/Прізвище
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                int temp = Convert.ToInt32(textBox5.Text);//Номер студента для видалення
                Form3 fm3 = new Form3();
                form1 = this;
                fm3.ShowDialog();
                string nm, snm;
                if (fm3.DialogResult == DialogResult.OK)
                {
                    nm = fm3.textBox1.Text;//Нове імя
                    snm = fm3.textBox2.Text;//Прізвище
                    students[temp - 1].name = nm;
                    students[temp - 1].surname = snm;
                    BindingSource binding = new BindingSource();
                    binding.DataSource = students;
                    dataGridView1.DataSource = binding;
                }
                if (fm3.DialogResult == DialogResult.Cancel)
                    fm3.Close();
                
            }
            catch
            {
                MessageBox.Show("Немає такого студента!", "Form Closing",
                               MessageBoxButtons.YesNo,
                              MessageBoxIcon.Question);
            }
        }
        //Додати предмет
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                string itm = textBox6.Text;//Назва нового предмету
                for(int i = 0; i < col_std; i++)
                {
                    students[i].grades.Add(new Grade(itm) { });
                }

            }
            catch
            {
                MessageBox.Show("Невірно введені дані!", "Form Closing",
                               MessageBoxButtons.YesNo,
                              MessageBoxIcon.Question);
            }
        }
        //Видалити предмет
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                string itm = textBox7.Text;//Назва предмета
                for (int i = 0; i < col_std; i++)
                {
                    students[i].grades.RemoveAll(x => x.item == itm);
                }

            }
            catch
            {
                MessageBox.Show("Невірно введені дані!", "Form Closing",
                               MessageBoxButtons.YesNo,
                              MessageBoxIcon.Question);
            }
        }
        //Змінити оцінку або предмет
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                Form4 fm4 = new Form4();
                form1 = this;
                fm4.ShowDialog();
                int std, grd;//студент, оцінка
                string itm, itm2;// предмет
                if (fm4.DialogResult == DialogResult.OK)
                {
                    std = Convert.ToInt32(fm4.textBox1.Text);//Студент
                    itm = fm4.textBox2.Text;//Предмет
                    grd = Convert.ToInt32(fm4.textBox3.Text);//Оцінка
                    int i = students[std-1].grades.FindIndex(x => x.item == itm);
                    students[std-1].grades[i].grade = grd;
                   
                }
                if (fm4.DialogResult == DialogResult.Yes)
                {
                    std = Convert.ToInt32(fm4.textBox1.Text);//Студент
                    itm = fm4.textBox2.Text;//Предмет
                    itm2 = fm4.textBox3.Text;//Нова назва предмету
                    int i = students[std - 1].grades.FindIndex(x => x.item == itm);
                    students[std - 1].grades[i].item = itm2;

                }
                if (fm4.DialogResult == DialogResult.Cancel)
                    fm4.Close();

            }
            catch
            {
                MessageBox.Show("Невірно введені дані!", "Form Closing",
                               MessageBoxButtons.YesNo,
                              MessageBoxIcon.Question);
            }
        }
        //Xml-серіалізація:
        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(List<Student>));
                using (var file = new FileStream("file1.xml", FileMode.Create))
                {
                    serializer.Serialize(file, students);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Form Closing",
                               MessageBoxButtons.YesNo,
                              MessageBoxIcon.Question);
            }
        }
        //Xml-десеріалізація:
        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(List<Student>));
                using (var file = new FileStream("file1.xml", FileMode.Open))
                {
                   students = serializer.Deserialize(file) as List<Student>;
                }
                BindingSource binding = new BindingSource();
                binding.DataSource = students;
                dataGridView1.DataSource = binding;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Form Closing",
                               MessageBoxButtons.YesNo,
                              MessageBoxIcon.Question);
            }
        }
    }
}
