using System;
using System.Collections.Generic;
using System.Linq;

class Товар
{
    public string Назва { get; set; }
    public decimal Ціна { get; set; }
    public string Опис { get; set; }
    public string Категорія { get; set; }
    public double Рейтинг { get; set; }

    public Товар(string назва, decimal ціна, string опис, string категорія, double рейтинг)
    {
        Назва = назва;
        Ціна = ціна;
        Опис = опис;
        Категорія = категорія;
        Рейтинг = рейтинг;
    }
}

class Користувач
{
    public string Логін { get; set; }
    public string Пароль { get; set; }
    public List<Замовлення> ІсторіяПокупок { get; set; }

    public Користувач(string логін, string пароль)
    {
        Логін = логін;
        Пароль = пароль;
        ІсторіяПокупок = new List<Замовлення>();
    }
}

class Замовлення
{
    public List<Товар> Товари { get; set; }
    public List<int> Кількість { get; set; }
    public Користувач Користувач { get; set; }
    public decimal ЗагальнаВартість { get; set; }
    public string Статус { get; set; }

    public Замовлення(List<Товар> товари, List<int> кількість, Користувач користувач)
    {
        Товари = товари;
        Кількість = кількість;
        Користувач = користувач;
        ЗагальнаВартість = Товари.Zip(Кількість, (товар, кільк) => товар.Ціна * кільк).Sum();
        Статус = "В обробці";
    }
}

interface ISearchable
{
    List<Товар> ПошукЗаЦіною(decimal мінЦіна, decimal максЦіна);
    List<Товар> ПошукЗаКатегорією(string категорія);
    List<Товар> ПошукЗаРейтингом(double мінРейтинг);
}

class Магазин : ISearchable
{
    private List<Користувач> користувачі = new List<Користувач>();
    private List<Товар> товари = new List<Товар>();
    private List<Замовлення> замовлення = new List<Замовлення>();

    public void ДодатиКористувача(Користувач користувач)
    {
        користувачі.Add(користувач);
    }

    public void ДодатиТовар(Товар товар)
    {
        товари.Add(товар);
    }

    public Замовлення СтворитиЗамовлення(List<Товар> товари, List<int> кількість, Користувач користувач)
    {
        var замовлення = new Замовлення(товари, кількість, користувач);
        return замовлення;
    }

    public List<Товар> ПошукЗаЦіною(decimal мінЦіна, decimal максЦіна)
    {
        return товари.Where(товар => товар.Ціна >= мінЦіна && товар.Ціна <= максЦіна).ToList();
    }

    public List<Товар> ПошукЗаКатегорією(string категорія)
    {
        return товари.Where(товар => товар.Категорія == категорія).ToList();
    }

    public List<Товар> ПошукЗаРейтингом(double мінРейтинг)
    {
        return товари.Where(товар => товар.Рейтинг >= мінРейтинг).ToList();
    }
}
