using System;

// Інтерфейс
interface ITransformation
{
    void SetNumbers(params int[] coefficients);
    void Math(int x, int y, int z = 0);
}

// Клас для 2D трансформацій
class T2DInterface : ITransformation
{
    protected int a11, a12, a13, a21, a22, a23;

    public void SetNumbers(params int[] coefficients)
    {
        if (coefficients.Length != 6)
        {
            Console.WriteLine("Помилка! Для 2D потрібно 6 коефіцієнтів.");
            return;
        }

        a11 = coefficients[0];
        a12 = coefficients[1];
        a13 = coefficients[2];
        a21 = coefficients[3];
        a22 = coefficients[4];
        a23 = coefficients[5];
    }

    public void Math(int x, int y, int z = 0)
    {
        int tempx = a11 * x + a12 * y + a13;
        int tempy = a21 * x + a22 * y + a23;
        Console.WriteLine($"Результат 2D: x = {tempx}, y = {tempy}");
    }
}

// Клас для 3D трансформацій
class T3DInterface : T2DInterface, ITransformation
{
    private int a14, a24, a31, a32, a33, a34;

    public new void SetNumbers(params int[] coefficients)
    {
        if (coefficients.Length != 12)
        {
            Console.WriteLine("Помилка! Для 3D потрібно 12 коефіцієнтів.");
            return;
        }

        base.SetNumbers(coefficients[0], coefficients[1], coefficients[2], coefficients[3], coefficients[4], coefficients[5]);

        a14 = coefficients[6];
        a24 = coefficients[7];
        a31 = coefficients[8];
        a32 = coefficients[9];
        a33 = coefficients[10];
        a34 = coefficients[11];
    }

    public new void Math(int x, int y, int z = 0)
    {
        if (z == 0)
        {
            base.Math(x, y);
        }
        else
        {
            int tempx = a11 * x + a12 * y + a13 * z + a14;
            int tempy = a21 * x + a22 * y + a23 * z + a24;
            int tempz = a31 * x + a32 * y + a33 * z + a34;
            Console.WriteLine($"Результат 3D: x = {tempx}, y = {tempy}, z = {tempz}");
        }
    }
}

class ProgramInterface
{
    static void Main(string[] args)
    {
        ITransformation transformation;

        Console.WriteLine("Оберіть тип трансформації (2D або 3D): ");
        string choice = Console.ReadLine();

        if (choice == "2D")
        {
            transformation = new T2DInterface();
            transformation.SetNumbers(1, 2, 3, 4, 5, 6);
        }
        else if (choice == "3D")
        {
            transformation = new T3DInterface();
            transformation.SetNumbers(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
        }
        else
        {
            Console.WriteLine("Неправильний вибір!");
            return;
        }

        Console.WriteLine("Введіть координати:");
        try
        {
            int x = int.Parse(Console.ReadLine());
            int y = int.Parse(Console.ReadLine());
            if (transformation is T3DInterface)
            {
                int z = int.Parse(Console.ReadLine());
                transformation.Math(x, y, z);
            }
            else
            {
                transformation.Math(x, y);
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Помилка вводу! Введіть числа.");
        }
    }
}
