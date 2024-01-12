using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class ShopController
    {

        public static Home Window { get; set; }

        public List<KioskProduct> Products { get; set; }

        public void Start()
        {
            // Stel de prijslijst in aan de rechter kant.
            ShopManager.SetShopPriceList("Prijzen:\n");


            // Stel de bon in onderaan het scherm
            
            

            // Vul de productlijst met producten
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto 10x15", Description = "Desc", Price = 59.99f });
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto 40x60", Description = "Desc", Price = 129.99f });
            foreach (KioskProduct product in ShopManager.Products)
            {
                ShopManager.AddShopPriceList($"{product.Name}: {product.Description} \nPrijs: {product.Price}\n\n");
            }

            // Update dropdown met producten
            ShopManager.UpdateDropDownProducts();
        }

        private float totalAmount = 0.0f; // slaat de aantal op zodat het kan opgeteld worden
                                          
        public void AddButtonClick() // Wordt uitgevoerd wanneer er op de Toevoegen knop is geklikt
        {
            KioskProduct product = ShopManager.GetSelectedProduct();
            float totalPrice = (float)(product.Price * ShopManager.GetAmount());

            // telt het totaal op
            totalAmount += totalPrice;

            ShopManager.AddShopReceipt($"Foto ID: {ShopManager.GetFotoId()}\n");
            ShopManager.AddShopReceipt($"Fotonummer: {product.Name}\n");
            ShopManager.AddShopReceipt($"Aantal: {ShopManager.GetAmount()}\n");
            ShopManager.AddShopReceipt($"Eindbedrag: €{totalAmount}\n");
        }

        // Wordt uitgevoerd wanneer er op de Resetten knop is geklikt
        public void ResetButtonClick()
        {
            ShopManager.SetShopReceipt("Eindbedrag: \n€"); 
        }

        // Wordt uitgevoerd wanneer er op de Save knop is geklikt
        public void SaveButtonClick()
        {
            File.WriteAllText(@"Receipt.txt", ShopManager.GetShopReceipt());

        }

    }
}
