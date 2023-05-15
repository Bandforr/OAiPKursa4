using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;

class Program
{
    static double SimpsonIntegration(double a, double b, int n, Function func)
    {
        double[,] arr = new double[n + 1, 2]; //массив для вывода в качестве таблицы
        arr[0, 0] = a; arr[n, 0] = b; //записываем yзлы а и b 
        arr[0, 1] = func.calculate(a); arr[n,1] = func.calculate(b); //записываем значение фyнкции в yзле а и b 

        double sum = arr[0, 1] + arr[n, 1]; //Сумма первого и последнего значения f(x)
        double h = (b - a) / n; // длина каждого из маленьких отрезков или шаг
        for (int i = 2; i < n; i += 2) // Сумма четных f(x)
        {
            arr[i, 0] = a + i * h; //вычисляем yзел
            arr[i, 1] = func.calculate(arr[i, 0]);  //вычисляем значение фyнкции в этом yзле
            sum += 2 * arr[i, 1];
        }
        for (int i = 1; i < n; i += 2) // Сумма нечетных f(x)
        {
            arr[i, 0] = a + i * h; 
            arr[i, 1] = func.calculate(arr[i, 0]);  
            sum += 4 * arr[i, 1];
        }
        TableOutput(arr, n); // выводим таблицy
        return sum * h / 3; // Возвращаем значение интеграла
    }
    static void TableOutput(double[,] arr, int n)
    {
        Console.WriteLine("Расчётная таблица:\n i | x | f(x)");
        for (int i = 0; i < n+1; i++)
        {
            Console.Write("{0,3}|{1,3}|{2}\n", i, arr[i,0],arr[i,1]);
        }
        Console.WriteLine();
    }

    static void Main(string[] args)
    {
        bool isCallSuccessful = License.iConfirmNonCommercialUse("John Doe");
        bool isConfirmed = License.checkIfUseTypeConfirmed();
        String message = License.getUseTypeConfirmationMessage();

        string input = "";
        double a = 0, b = 0;
        int n = 0;
        bool inputCheck = false;
        while (!inputCheck)
        {
            Console.Write("Введите подинтегральное выражение (интеграл по x, точка для десятичных дробей. Пример: 'sin(√(3.3*x))^2-4'): "); 
            input = Console.ReadLine();
            Console.Write("Введите нижнюю границу интегрирования a = ");
            a = Convert.ToDouble(Console.ReadLine());
            Console.Write("Введите верхнюю границу интегрирования b = ");
            b = Convert.ToDouble(Console.ReadLine());
            Console.Write("Введите чётно число отрезков разбиения n = ");
            n = Convert.ToInt32(Console.ReadLine());
            if (a > b || n % 2 != 0) Console.WriteLine("Проверьте ввод. a должно быть меньше b, n должно быть чётным.");
            else inputCheck = true;
        }

        Function func = new Function("f(x)=" + input); // создаем функцию
        double result = SimpsonIntegration(a, b, n, func);

        Console.WriteLine($"Интеграл {input} по dx на отрезке [{a};{b}] премерно равен: {result}");
        Console.ReadKey();
    }
}