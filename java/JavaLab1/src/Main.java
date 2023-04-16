import java.util.Comparator;
import java.util.HashSet;
import java.util.Set;
import java.util.TreeSet;

// Press ⇧ twice to open the Search Everywhere dialog and type `show whitespaces`,
// then press Enter. You can now see whitespace characters in your code.
public class Main {
    public static void main(String[] args) {
        Set<Mage> mages;
        String sortMode = "natural";

        // Tworzenie obiektów klasy Mage
        Mage mage1 = new Mage("Gandalf", 20, 100.0);
        Mage mage2 = new Mage("Saruman", 18, 95.0);
        Mage mage3 = new Mage("Merlin", 15, 85.0);
        Mage mage4 = new Mage("Harry Potter", 10, 80.0);
        Mage mage5 = new Mage("Dumbledore", 20, 90.0);
        Mage mage6 = new Mage("Gargamel", 5, 70.0);
        Mage mage7 = new Mage("Hermione Granger", 12, 75.0);
        Mage mage8 = new Mage("Mickey Mouse", 7, 60.0);
        Mage mage9 = new Mage("Dr. Strange", 18, 98.0);
        Mage mage10 = new Mage("Gonzo", 3, 50.0);

        mage1.addApprentice(mage2);
        mage1.addApprentice(mage3);
        mage3.addApprentice(mage10);
        mage10.addApprentice(mage9);
        mage5.addApprentice(mage4);
        mage5.addApprentice(mage7);
        mage9.addApprentice(mage8);

        if (sortMode.equals("natural")) {
            mages = new TreeSet<>();
        } else if (sortMode.equals("alternative")) {
            Comparator<Mage> alternativeComparator = Comparator.comparing(Mage::getLevel)
                    .thenComparing(Mage::getPower);
            mages = new TreeSet<>(alternativeComparator);
        } else {
            mages = new HashSet<>();
        }

        mages.add(mage1);
        mages.add(mage2);
        mages.add(mage3);
        mages.add(mage4);
        mages.add(mage5);
        mages.add(mage6);
        mages.add(mage7);
        mages.add(mage8);
        mages.add(mage9);
        mages.add(mage10);


        for (Mage mage : mages) {
            System.out.println(mage);
        }


        System.out.println("\n\n\n");
        mage1.print();
    }
}

