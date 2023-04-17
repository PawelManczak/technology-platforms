import java.util.*;

// Press ⇧ twice to open the Search Everywhere dialog and type `show whitespaces`,
// then press Enter. You can now see whitespace characters in your code.
public class Main {
    public static void main(String[] args) {
        String sortMode = "alternative";//alternative // natural

        Mage mage1 = new Mage("Gandalf", 20, 100.0, sortMode);
        Mage mage2 = new Mage("Saruman", 18, 95.0, sortMode);
        Mage mage3 = new Mage("Merlin", 15, 85.0, sortMode);
        Mage mage4 = new Mage("Harry Potter", 10, 80.0, sortMode);
        Mage mage5 = new Mage("Dumbledore", 20, 90.0, sortMode);
        Mage mage6 = new Mage("Gargamel", 5, 70.0, sortMode);
        Mage mage7 = new Mage("Hermione Granger", 12, 75.0, sortMode);
        Mage mage8 = new Mage("Mickey Mouse", 7, 60.0, sortMode);
        Mage mage9 = new Mage("Dr. Strange", 18, 98.0, sortMode);
        Mage mage10 = new Mage("Gonzo", 3, 50.0, sortMode);

        mage1.addApprentice(mage2);
        mage1.addApprentice(mage3);
        mage1.addApprentice(mage4);
        mage1.addApprentice(mage5);

        mage5.addApprentice(mage6);
        mage5.addApprentice(mage7);
        mage5.addApprentice(mage8);
        mage5.addApprentice(mage9);


        /*mage3.addApprentice(mage10);
        mage10.addApprentice(mage9);
        mage5.addApprentice(mage4);
        mage5.addApprentice(mage7);
        mage9.addApprentice(mage8);*/

        System.out.println("print info about Gandalf");
        mage1.print();
        System.out.println("\n");

        System.out.println("Gandalf statistics");
        mage1.printStatistics();

        System.out.println("\namount of Gandalfs apprentices");
        System.out.println(mage1.getAmountOfApprentice());

        System.out.println("\napprentices of Gandalf");
        mage1.printApprentices();
    }
}

