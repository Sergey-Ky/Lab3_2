using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Lab3_2
{
    public partial class Form1
    {
        private ListBox listBox;
        private Button generateButton;
        private Button readButton;
        private Button filterButton;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            listBox = new System.Windows.Forms.ListBox();
            generateButton = new System.Windows.Forms.Button();
            readButton = new System.Windows.Forms.Button();
            filterButton = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // listBox
            // 
            listBox.FormattingEnabled = true;
            listBox.Location = new System.Drawing.Point(10, 10);
            listBox.Name = "listBox";
            listBox.Size = new System.Drawing.Size(760, 364);
            listBox.TabIndex = 0;
            // 
            // generateButton
            // 
            generateButton.Location = new System.Drawing.Point(10, 380);
            generateButton.Name = "generateButton";
            generateButton.Size = new System.Drawing.Size(180, 30);
            generateButton.TabIndex = 1;
            generateButton.Text = "Generate and Save Trains";
            generateButton.UseVisualStyleBackColor = true;
            generateButton.Click += generateButton_Click;
            // 
            // readButton
            // 
            readButton.Location = new System.Drawing.Point(200, 380);
            readButton.Name = "readButton";
            readButton.Size = new System.Drawing.Size(180, 30);
            readButton.TabIndex = 2;
            readButton.Text = "Read and Display Trains";
            readButton.UseVisualStyleBackColor = true;
            readButton.Click += readButton_Click;
            // 
            // filterButton
            // 
            filterButton.Location = new System.Drawing.Point(460, 380);
            filterButton.Name = "filterButton";
            filterButton.Size = new System.Drawing.Size(180, 30);
            filterButton.TabIndex = 3;
            filterButton.Text = "Filter by Trains numbers";
            filterButton.UseVisualStyleBackColor = true;
            filterButton.Click += filterButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(780, 420);
            Controls.Add(filterButton);
            Controls.Add(readButton);
            Controls.Add(generateButton);
            Controls.Add(listBox);
            Text = "Train App";
            ResumeLayout(false);
        }

        private void InitializeUI()
        {
            listBox.Items.Clear();
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            List<TRAIN> trains = GenerateTrains();
            SaveTrainsToFile(trains);
            MessageBox.Show("Trains generated and saved successfully.");
        }

        private List<TRAIN> GenerateTrains()
        {
            Random random = new Random();
            List<TRAIN> trains = new List<TRAIN>();

            string[] destinations =
            {
                "Ветрогонск-Центральный",
                "Лукоморье-Приморское",
                "Чернореченск-Товарный",
                "Златогорье-Восточное",
                "Белозерск-Пассажирский",
                "Каменистый Перевал",
                "Светлоград-Южный",
                "Туманный Бор",
                "Озёрный Край",
                "Староключевск",
                "Новодолинск-Западный",
                "Гремячий Ключ",
                "Железнодольск",
                "Мостовик",
                "Приволье-Сортировочная",
                "Звёздный Путь",
                "Крутые Вершины",
                "Тихоборье",
                "Рубежное",
                "Серебряные Пруды",
                "Верхнедонской",
                "Лесоповальная",
                "Быстрогорск",
                "Чистополье-Тупик",
                "Краснокаменка",
                "Дальнеозёрный",
                "Малахов Угол",
                "Подгорье-Ветвистое",
                "Стрежевой Разъезд",
                "Усть-Камчадальск"
            };

            for (int i = 0; i < 10; i++)
            {
                string trainNumber = random.Next(0, 1000).ToString("D3");
                string destination = destinations[random.Next(30)];
                string departureTime = $"{random.Next(1950, 2000)} {random.Next(1, 13)} {random.Next(1, 29)}";

                trains.Add(new TRAIN
                {
                    TrainNumber = trainNumber,
                    Destination = destination,
                    DepartureTime = departureTime,
                });
            }

            return trains;
        }

        private void SaveTrainsToFile(List<TRAIN> trains)
        {
            string filePath = "C:\\Users\\ser20\\OneDrive\\Рабочий стол\\Lab3_2\\trains.txt";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var train in trains)
                {
                    writer.WriteLine($"{train.TrainNumber},{train.Destination},{train.DepartureTime}");
                }
            }
        }

        private void readButton_Click(object sender, EventArgs e)
        {
            listBox.Items.Clear();
            List<TRAIN> trains = ReadTrainsFromFile();
            foreach (var train in trains)
            {
                listBox.Items.Add($"{train.TrainNumber} {train.Destination}, {train.DepartureTime}");
            }
        }

        private List<TRAIN> ReadTrainsFromFile()
        {
            List<TRAIN> trains = new List<TRAIN>();
            string filePath = "C:\\Users\\ser20\\OneDrive\\Рабочий стол\\Lab3_2\\trains.txt";

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 3)
                        {
                            TRAIN train = new TRAIN
                            {
                                TrainNumber = parts[0],
                                Destination = parts[1],
                                DepartureTime = parts[2],
                            };
                            trains.Add(train);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("File does not exist.");
            }

            return trains;
        }

        private void filterButton_Click(object sender, EventArgs e)
        {
            listBox.Items.Clear();
            List<TRAIN> trains = ReadTrainsFromFile();
            trains.Sort((t1, t2) => t1.TrainNumber.CompareTo(t2.TrainNumber));

            foreach (var train in trains)
            {
                listBox.Items.Add($"{train.TrainNumber}, {train.Destination}, {train.DepartureTime}");
            }
        }

        public class TRAIN
        {
            public string Destination { get; set; }
            public string TrainNumber { get; set; }
            public string DepartureTime { get; set; }
        }
    }
}