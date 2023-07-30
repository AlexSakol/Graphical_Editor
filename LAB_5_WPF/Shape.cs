//КЛАСС ДЛЯ ФИГУРЫ
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;
using Microsoft.Win32;
using System.Text.Json;
using System.IO;

namespace LAB_5_WPF
{
    internal class Shape
    {
        public Shape()
        {
        }

        public Shape(int thickness, System.Windows.Media.Color? background, Color? foreground, int width, int height)
        {
            Thickness = thickness;
            Background = background;
            Foreground = foreground;
            Width = width;
            Height = height;
        }

        public int Thickness { get; set; }
        public Color? Background { get; set; }
        public Color? Foreground { get; set; }
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;
        //асинхронное сохранение в json
        async public void Save()
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Файлы (json)|*.json|Все файлы|*.*";
            if (fileDialog.ShowDialog() == true)
            {
                using (FileStream fs = new FileStream(fileDialog.FileName, FileMode.Create))
                {
                    await JsonSerializer.SerializeAsync<Shape>(fs, this);                    
                }
            }
            else return;
        }
        //асинхронное чтение из json
        async public static Task<Shape> Load()
        {
            Shape? shape;
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Файлы (json)|*.json|Все файлы|*.*";
            if (fileDialog.ShowDialog() == true)
            {
                using (FileStream fs = new FileStream(fileDialog.FileName, FileMode.Open))
                {
                    shape = await JsonSerializer.DeserializeAsync<Shape>(fs);
                }
                return shape;
            }
            else return null;
        }
        public void Draw(Canvas canvas, Point point)
        {
            Polygon polygon = new Polygon();
            polygon.Points.Add(point);
            polygon.Points.Add(new Point(point.X + Width / 2, point.Y + Height / 2));
            polygon.Points.Add(new Point(point.X + Width, point.Y));
            polygon.Points.Add(new Point(point.X + Width / 2, point.Y - Height/2));
            polygon.Fill = new SolidColorBrush((Color)Background);
            polygon.Stroke = new SolidColorBrush((Color)Foreground);
            polygon.StrokeThickness= Thickness;
            canvas.Children.Add(polygon);
        }
        public override string? ToString() => $"Thickness = {Thickness} Background = {Background} " +
            $"Foreground = {Foreground} Width ={Width} Height = {Height}";
        
    }
}
