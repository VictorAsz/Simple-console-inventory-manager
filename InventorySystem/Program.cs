using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Inventory inventory = new Inventory();
            bool exit = false;
           
            while (!exit)
            {
                Console.Clear();
                ShowMenu();
                MenuOption selectedOption = (MenuOption)GetMenuOption();

                switch (selectedOption) 
                {
                    case MenuOption.AdicionarItem:
                        AddItem(inventory);
                        break;
                    case MenuOption.EditarItem:
                        EditItem(inventory);
                        break;
                    case MenuOption.DeletarItem:
                        DeleteItem(inventory);
                        break;
                    case MenuOption.VerItens:
                        ShowItems(inventory);
                        break;
                    case MenuOption.Sair:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }

            }

        
        }

        static void CreateItem()
        {
            Console.WriteLine("Criando um novo item...");
            Console.WriteLine("Item criado!");
        }

        static void AddItem(Inventory inventory)
        {
            Console.WriteLine("Adicionar item do tipo arma:");
            Console.Write("Nome: ");
            string name = Console.ReadLine();

            Console.Write("Peso: ");
            float weight;
            while (!float.TryParse(Console.ReadLine(), out weight))
            {
                Console.WriteLine("Peso inválido. Tente novamente:");
            }

            Console.WriteLine("Condição (1 - Bom, 2 - Danificado, 3 - Quebrado):");
            ItemCondition condition;
            while (!Enum.TryParse(Console.ReadLine(), out condition) || !Enum.IsDefined(typeof(ItemCondition), condition))
            {
                Console.WriteLine("Condição inválida. Tente novamente:");
            }

            inventory.AddItem(new Weapon(name, weight, condition));
            Console.WriteLine("Item adicionado!");
        }

        static void EditItem(Inventory inventory)
        {
          

            if (inventory.Count() == 0)
            {
                Console.WriteLine("Não há itens para editar, pressione ENTER para voltar...");
                Console.ReadLine();
               
            }
            else { 
            Console.WriteLine("Editar Item:");
            inventory.ShowItems();
            Console.WriteLine("Escolha o número do item para editar:");
               
            int index;
            while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > inventory.Count())
            {
                Console.WriteLine("Número do item inválido. Tente novamente:");
            }

            Console.Write("Novo nome: ");
            string newName = Console.ReadLine();

            Console.Write("Novo peso: ");
            float newWeight;
            while (!float.TryParse(Console.ReadLine(), out newWeight))
            {
                Console.WriteLine("Peso inválido. Tente novamente:");
            }

            Console.WriteLine("Nova condição (1 - Bom, 2 - Danificado, 3 - Quebrado):");
            ItemCondition newCondition;
            while (!Enum.TryParse(Console.ReadLine(), out newCondition) || !Enum.IsDefined(typeof(ItemCondition), newCondition))
            {
                Console.WriteLine("Condição inválida. Tente novamente:");
            }

            inventory.EditItem(index - 1, newName, newWeight, newCondition);
            Console.WriteLine("Item editado!");
          
               }
            
        }

        static void DeleteItem(Inventory inventory)
        {
            Console.WriteLine("Deletar Item:");
            inventory.ShowItems();
            Console.WriteLine("Escolha o número do item para deletar:");

            int index;
            while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > inventory.Count())
            {
                Console.WriteLine("Número do item inválido. Tente novamente:");
            }

            inventory.DeleteItem(index - 1);
            Console.WriteLine("Item deletado!");
        }

        static void ShowItems(Inventory inventory)
        {
            if (inventory.Count() != 0)

            {
                inventory.ShowItems();
                Console.ReadLine();
                Console.WriteLine("Pressione ENTER para sair...");


            }
            else
            {
                Console.WriteLine("Nenhum item no inventário. Pressione ENTER para continuar...");
                Console.ReadLine();
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine("1. Adicionar Item");
            Console.WriteLine("2. Editar Item");
            Console.WriteLine("3. Deletar Item");
            Console.WriteLine("4. Ver Itens");
            Console.WriteLine("5. Sair");
        }

        static int GetMenuOption()
        {
            int option;
            while (!int.TryParse(Console.ReadLine(), out option) || option < 1 || option > 6)
            {
                Console.WriteLine("Opção inválida. Tente novamente:");
            }
            return option;
        }
    }

    enum MenuOption
    {
         
        AdicionarItem = 1,
        EditarItem = 2,
        DeletarItem = 3,
        VerItens = 4,
        Sair = 5,
    }

    public enum ItemCondition
    {
        Bom = 1,        
        Danificado = 2, 
        Quebrado = 3    
    }

    class Inventory
    {
        private List<Item> items = new List<Item>();

        public void AddItem(Item item)
        {
            items.Add(item);
        }

        public void EditItem(int index, string newName, float newWeight, ItemCondition newCondition)
        {
            if (index >= 0 && index < items.Count)
            {
                items[index].Name = newName;
                items[index].Weight = newWeight;
                items[index].Condition = newCondition;
            }
        }

        public void DeleteItem(int index)
        {
            if (index >= 0 && index < items.Count)
            {
                items.RemoveAt(index);
            }
        }

        public void ShowItems()
        {
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {items[i].Name} - Peso: {items[i].Weight}KG - Condição: {items[i].Condition}");
            }
        }

        public int Count() 
        {
            return items.Count;
        }

    }

    abstract class Item
    {
        private string name;
        private float weight;
        private ItemCondition condition;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public float Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public ItemCondition Condition
        {
            get { return condition; }
            set { condition = value; }
        }
    }

    class Weapon : Item
    {
        public Weapon(string name, float weight, ItemCondition condition)
        {
            Name = name;
            Weight = weight;
            Condition = condition;
        }
    }

    class Potion : Item 
        { 
            
            public Potion()
                {

                }
        
        }

}
