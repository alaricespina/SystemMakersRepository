
import magepack.*;
public class Input {
    public static void main (String args[]){
       Mage mage = new Mage();
       Mage.Sorcerer Decoretum = mage.new Sorcerer("Decoretum", 19, "Abyss", "Destruction & Alteration");
       Mage.Spellsword Pontrilius = mage.new Spellsword("Pontrilius", 19, "Lightning", "Blade, Destruction, & Alteration");

       Decoretum.Information();
       Decoretum.WarCry();
       System.out.println("");

       Pontrilius.WarCry();
       Pontrilius.Information();
       System.out.println("");

       //higher mages, public classes within a public main class, hence inner public classes
       HigherMages highmage = new HigherMages();
       HigherMages.ArchSorcerer DecoretumtheGreat = highmage.new ArchSorcerer("Decoretum",19,"Abyss","Destruction, Alteration, & Conjuration");
       HigherMages.Necromancer Absalon = highmage.new Necromancer("Absalon", 19, "Lightning", "Destruction & Restoration");
       DecoretumtheGreat.Arrogance();
       DecoretumtheGreat.Abilities();
       DecoretumtheGreat.Quote();
       Absalon.Quote();

       

       //abyss category
        AbyssGod DarkMother = new AbyssGod();
        AbyssAdept DarkAdept = new AbyssAdept("Malborn", "Destruction & Conjuration");
        DarkMother.WarCry();
        DarkAdept.WarCry();


    }
}