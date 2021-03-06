using System;
using System.Collections.Generic;

namespace mlaSharp
{
	/// <summary>
	/// A generic card.
	/// </summary>
	public class Card
	{
		
		public string Name { get; set;}
		public string Type { get; set;}
		public ManaCost Cost { get; set;}
		public string Text { get; set;}
		public Player Owner { get; set;}
		public Player Controller { get; set;}
		public ColorsEnum Colors { get; set;}
		public StatusEnum Status { get; set;}
		public int ControlTimestamp { get; set; }
		public long UniqueID { get; private set; }
		
		public List<Ability> ActivatedAbilities { get; private set;}
		public List<Ability> TriggeredAbilities { get; private set;}
		public List<Ability> StaticAbilities { get; private set;}
		
		private static long instanceCount = 0;
		
		public Card()
		{
			instanceCount++;
			this.UniqueID = instanceCount;
		}
		public Card (string name, string type, string manaCost, string text)
			:this()
		{
			ActivatedAbilities = new List<Ability>();
			TriggeredAbilities = new List<Ability>();
			StaticAbilities  = new List<Ability>();
			
			this.Name = name;
			this.Type = type;
			this.Cost = new ManaCost(manaCost);
			this.Text = text;
			this.Colors = this.Colors.SetColors (manaCost);
			this.Status = StatusEnum.Default;
			this.ControlTimestamp = Int32.MaxValue;
		}
		
		public override string ToString ()
		{
			if (Controller != null)
				return Controller.ToString() + "'s " + Name;
			
			if (Owner != null)
				return Owner.ToString() + "'s " + Name;
			
			return Name;
		}
		
		/// <summary>
		/// Enumeration of card statuses.  0 is:
		/// untapped, face up, phased in, and unflipped.
		/// </summary>
		[Flags]
		public enum StatusEnum
		{
			Default = 0,
			Tapped = 1,
			FaceDown = 2,
			PhasedOut = 4,
			Flipped = 8
		}
	}
	
	/// <summary>
	/// A Creature card with power and toughness.
	/// </summary>
	public class CreatureCard : Card
	{
		public int P {get; set;}
		public int T { get; set;}
		public int DamageMarked { get; set; }
		
		public CreatureCard(string name, string type, string manaCost, string text, int power,int toughness)
			: base(name,type,manaCost,text)
		{
			this.P = power;
			this.T = toughness;
		}
	}
	
	[System.AttributeUsage(System.AttributeTargets.Class)]
	public class CardAttribute : System.Attribute
	{
		private string _name;
		
		public CardAttribute(string name)
		{
			_name = name;	
		}
	}
}

