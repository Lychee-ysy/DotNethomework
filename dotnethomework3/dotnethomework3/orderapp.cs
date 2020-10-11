using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using System.Windows;
namespace OrderApp
{
    class orderapp
    {
        static void Main(string[] args)
        {
            Allorder a = new Allorder();
            bool judge_ = true;
            while(judge_)
            {
                Console.WriteLine("输入1增加订单\r\n输入2删除订单\r\n输入3查询订单\r\n输入4显示所有订单\r\n输入5根据订单号为订单排序,\r\n输入6序列化订单\r\n输入7反序列化订单\r\n输入8退出");
                string choose = Console.ReadLine();
                switch (choose)
                {
                    case "1":a.addOrder();break;
                    case "2":a.removeOrder();break;
                    case "3":Console.WriteLine("输入1根据订单金额查询订单，输入2根据客户名查询订单");
                        int i = Convert.ToInt32(Console.ReadLine());
                        a.searchOrder(i);
                        break;
                    case "4":a.showOrder();break;
                    case "5":a.orders.Sort();break;
                    case "6":a.export();break;
                    case "7":a.import();break;
                    case "8":judge_ = false;break;
                    default:Console.WriteLine("输入错误！");break;
                }

            }
        }
    }
    [Serializable]
    public class Allorder : IorderService//所有订单操作
    {
        public List<myorder> orders = new List<myorder>();
        public Allorder(){}
        public void export()
        {
            XmlSerializer a = new XmlSerializer(typeof(List<myorder>));
            using (FileStream b = new FileStream("order.xml", FileMode.Create))
            {
                a.Serialize(b, this.orders);
            }
            Console.WriteLine("完成序列化");
        }
        public void import()
        {
            try 
            {
                XmlSerializer a = new XmlSerializer(typeof(List<myorder>));
                using (FileStream b = new FileStream("order.xml", FileMode.Open))
                {
                    List<myorder> c = (List<myorder>)a.Deserialize(b);
                    Console.WriteLine("反序列化结果：");
                    foreach(myorder d in c)
                    {
                        Console.WriteLine("订单     客户     金额       日期    ");
                        Console.WriteLine("-------------------------------------");
                        Console.WriteLine("{0}      {1}       {2}        {3}", d.ID, d.Customer, d.Date, d.Money);
                        d.showOrderItem();
                    }
                }
            }
            catch
            {
                Console.WriteLine("序列化操作错误！");
            }
        }
        public void showOrder()
        {
            foreach(myorder a in this.orders)
            {
                Console.WriteLine("订单号       客户       日期       总金额：");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("{0}          {1}        {2}          {3}", a.ID, a.Customer, a.Date, a.Money);
                a.showOrderItem();
            }
        }
        public void addOrder()
        {
            try
            {
                Console.WriteLine("请输入订单号：");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("请输入客户名：");
                string customer = Console.ReadLine();
                Console.WriteLine("请输入日期：");
                string date = Console.ReadLine();
                myorder a = new myorder(id, customer, date);
                bool judge = true;
                bool same = false;
                foreach (myorder m in this.orders)
                {
                    if (m.Equals(a)) same = true;
                }
                if (same) Console.WriteLine("订单号重复");
                else
                {
                    while (judge && !same)
                    {
                        Console.WriteLine("请输入订单项：");
                        string name = Console.ReadLine();
                        Console.WriteLine("请输入订单数量：");
                        int number = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("请输入订单单价：");
                        double price = Convert.ToDouble(Console.ReadLine());
                        a.addOrderItem(name, number, price);
                        Console.WriteLine("是否继续添加订单？请输入 是 或 否 ");
                        string e = Console.ReadLine();
                        if (e == "否") judge = false;
                        else if (e == "是") continue;
                        else if (e != "是" && e != "否")
                        {
                            Exception x = new Exception();
                            throw x;
                        }
                    }
                }
                orders.Add(a);
                a.getAllPrice();
                Console.WriteLine("输入成功！");
                Console.WriteLine("----------------------");
            }
            catch
            {
                Console.WriteLine("输入错误！");
            }
        }
        public void removeOrder()
        {
            try
            {
                Console.WriteLine("请输入要删除的订单号：");
                int no_ = Convert.ToInt32(Console.ReadLine());
                int index = 0;
                foreach (myorder m in this.orders)
                {
                    while (m.ID == no_)
                    {
                        index = this.orders.IndexOf(m);//IndexOf函数，反汇第一次找到目标时的位置
                    }
                }
                    Console.WriteLine("输入1删除订单，输入2删除订单明细（即清空指定订单的内容）");
                    int i = Convert.ToInt32(Console.ReadLine());
                    switch (i)
                    {
                        case 1:this.orders.RemoveAt(index);Console.WriteLine("删除成功！"); break;
                        case 2:this.orders[index].showOrderItem();this.orders[index].removeOrderItem();break;
                        default:Console.WriteLine("输入错误！");break;
                    }
            }
            catch
            {
                Console.WriteLine("删除失败！");
            }
        }
        /*public void searchOrder(int i)
        {
            try
            {
                Console.WriteLine("请输入要查询的订单号：");
                int n = Convert.ToInt32(Console.ReadLine());
                int index = 0;
                foreach(order s in this.orders)
                {
                    if (s.ID == n) index = this.orders.IndexOf(s);
                }
                this.orders[index].showOrderItem();
            }
            catch
            {
                Console.WriteLine("寻找失败!");
            }
        }*/
        public void searchOrder(int i)  //查询订单
        {
            try
            {
                switch (i)
                {
                    case 1:
                        int minNum, maxNum;
                        Console.WriteLine("输入要查询的最小金额：");
                        minNum = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("输入要查询的最大金额：");
                        maxNum = Convert.ToInt32(Console.ReadLine());


                        var query1 = from s1 in orders
                                     where maxNum > s1.Money
                                     orderby s1.Money
                                     select s1;
                        var query3 = from s3 in query1
                                     where s3.Money > minNum
                                     orderby s3.Money
                                     select s3;

                        List<myorder> a1 = query3.ToList();

                        foreach (myorder b1 in a1)
                        {
                            Console.WriteLine("订单号 客户 日期 总金额");
                            Console.WriteLine("----------------------------");
                            Console.WriteLine("{0} {1} {2} {3}", b1.ID, b1.Customer, b1.Date, b1.Money);
                            b1.showOrderItem();
                        }
                        break;
                    case 2:

                        Console.WriteLine("输入客户名称：");
                        string name1 = Console.ReadLine();

                        var query2 = from s2 in orders
                                     where s2.Customer == name1
                                     orderby s2.Money
                                     select s2;
                        List<myorder> a2 = query2.ToList();

                        foreach (myorder b2 in a2)
                        {
                            Console.WriteLine("订单号 客户 日期 总金额");
                            Console.WriteLine("----------------------------");
                            Console.WriteLine("{0} {1} {2} {3}", b2.ID, b2.Customer, b2.Date, b2.Money);
                            b2.showOrderItem();
                        }
                        break;
                    default: Console.WriteLine("输入错误"); break;

                }
            }
            catch
            {
                Console.WriteLine("输入错误");
            }
        }
    }
    [Serializable]
    public class myorder : IComparable//单个订单
    {
        
        public int ID { get; set; }
        public string Customer { get; set; }
        public double Money { get; set; }
        public string Date { get; set; }
        public List<orderitem> orderItems = new List<orderitem>();
        public myorder()
            {
            this.ID = 0;
            this.Customer = string.Empty;
            this.Money = 0;
            this.Date = string.Empty;
            }
        public int CompareTo(object obj)//compareTo方法
        {
            myorder a = obj as myorder;
            return this.ID.CompareTo(a.ID);
        }
        public override bool Equals(object obj)
        {
            myorder a = obj as myorder;
            return this.ID == a.ID;
        }
        public override int GetHashCode()
        {
            return Convert.ToInt32(ID);
        }
        public myorder(int ID, string customer,string Date)
        {
            this.ID = ID;
            this.Customer = customer;
            this.Date = Date;
        }
        public void getAllPrice()
        {
            double i = 0;
            foreach(orderitem a in orderItems)
            {
                i = i + a.getPrice();
            }
            this.Money = i;
        }
        public void addOrderItem(string name,int number,double price)
        {
            orderitem a = new orderitem(name, number, price);
            this.orderItems.Add(a);
        }
        public void removeOrderItem()
        {
            try
            {
                Console.WriteLine("请输入要删除的订单号：");
                int a = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("删除成功！");
                Console.WriteLine("----------------------");
            }
            catch
            {
                Console.WriteLine("输入序号错误！");
            }
        }
        public void showOrderItem()//展示订单
        {
            Console.WriteLine("订单序列号  名称  个数  单价");
            foreach(orderitem a in this.orderItems)
            {
                Console.WriteLine("---------------------------");
                Console.WriteLine("{0} {1} {2} {3}", this.orderItems.IndexOf(a), a.Name, a.Number, a.Price);
            }
        }
    }
    [Serializable]
    public class orderitem           //订单明细
    {
        private string name;
        private int number;
        private double price;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public int Number
        {
            get
            {
                return number;
            }
            set
            {
                if (number >= 0) number = value;
                Console.WriteLine("数量必须大于0！");
            }
        }
        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }
        public orderitem()//无参构造函数
        {
            this.name = string.Empty;
            this.number = 0;
            this.price = 0;
        }
        public orderitem(string name,int number,double price)
        {
            this.number = number;
            this.name = name;
            this.price = price;
        }
        public double getPrice()
        {
            return this.number * this.price;
        }
    }

    public interface IorderService//包含所有订单功能的接口
    {
        void addOrder();
        void removeOrder();
        void searchOrder(int i);
        void export();
        void import();
    }

}
