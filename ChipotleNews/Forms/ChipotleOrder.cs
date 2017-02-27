using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;

namespace ChipotleNews.Forms
{
    // **Note: 0 value in enums is reserved for unkown values, either supply an explicit one or start enumeration at 1
    public enum MainOptions
    {
        Unknown,
        Burrito,
        Bowl,
        Tacos
    }
    public enum FillingOptions
    {
        Unknown,
        Steak,
        Chicken,
        Carnitas,
        Barbacoa,
        Chorizo,
        Sofritas,
        Veggie
    }
    public enum RiceOptions
    {
        Unknown,
        White,
        Brown,
        None
    }
    public enum BeanOptions
    {
        Unknown,
        Pinto,
        Black,
        None
    }
    public enum TopingOptions
    {
        Unknown,
        Guacamole,
        FreshSalsa,
        CornSalsa,
        GreenSalsa,
        RedSalsa,
        SourCream,
        Fajitas,
        Cheese,
        Lettuce
    }

    [Serializable]
    public class ChipotleOrder
    {
        [Prompt("What would you Like? {||}")]
        public MainOptions Option;

        [Prompt("How many?")]
        public int numberOrders;

        [Prompt("What kind of Meat would you like? We also have Veggies {||}")]
        public FillingOptions FillingOption;

        [Prompt("White or Brown Rice? {||}")]
        public RiceOptions RiceOption;

        [Prompt("What kind of Beans?{||}")]
        public BeanOptions BeanOption;

        [Prompt("We also have Salsa, Sour Cream, Cheese and Lettuce {||}")]
        public List<TopingOptions> Topings;

        public static IForm<ChipotleOrder> BuildForm()
        {
            return new FormBuilder<ChipotleOrder>()
                .Message("Hi! I'm ChipotleBot, Can I take your Order?")
                .Build();
        }
    }
}