import java.util.*;

public class Mage implements Comparable<Mage> {
    private String name;
    private int level;
    private double power;
    private Set<Mage> apprentices;
    private Map<Mage, Integer> map;
    private String sortMode;

    public Mage(String name, int level, double power, String sortMode) {
         this.name = name;
         this.level = level;
         this.power = power;

         this.sortMode = sortMode;
        if (sortMode.equals("natural")) {
            apprentices = new TreeSet<>();
        } else if (sortMode.equals("alternative")) {
            Comparator<Mage> alternativeComparator = Comparator.comparing(Mage::getLevel)
                    .thenComparing(Mage::getPower);
            apprentices = new TreeSet<>(alternativeComparator);
        } else {
            apprentices = new HashSet<>();
        }
    }

    @Override
    public boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof Mage mMage)) {
            return false;
        }

        return Objects.equals(mMage.name, this.name) && mMage.level == this.level && mMage.power == this.power && mMage.apprentices == this.apprentices;
    }

    @Override
    public int hashCode() {
        return Objects.hash(name, level, power, apprentices);
    }

    @Override
    public int compareTo(Mage o) {
        if(this.name.compareTo(o.name) > 0) return 1;
        if(this.name.compareTo(o.name) < 0) return -1;
        if(this.level > o.level) return 1;
        if(this.level < o.level) return -1;
        return Double.compare(this.power, o.power);
    }

    @Override
    public String toString() {
        return "Mage{" +
                "name='" + name + '\'' +
                ", level=" + level +
                ", power=" + power +
                '}';
    }

    public void print(){
        print(0);
    }
    private void print(int depth){

        System.out.println(this.toString());

        for(Mage mage : this.apprentices){
            for (int i = 0; i < depth+1; i++) {
                System.out.print("-");
            }
            mage.print(depth+1);
        }
    }

    public int getAmountOfApprentice(Mage mMage){
        int counter = 1;
        for(Mage mage : mMage.apprentices){
            counter += getAmountOfApprentice(mage);
        }
        return counter;
    }

    public int getAmountOfApprentice(){
        return getAmountOfApprentice(this) - 1;
    }
    public void addApprentice(Mage mag) {
        this.apprentices.add(mag);
    }

    public double getPower() {
        return power;
    }

    public int getLevel() {
        return level;
    }

    public void printStatistics(){

        if (sortMode.equals("natural")) {
            map = new TreeMap<>();
        } else if (sortMode.equals("alternative")) {
            map = new TreeMap<>();
        } else {
            map = new HashMap<>();
        }

        map.put(this, this.getAmountOfApprentice());
        get(this);

        for (Map.Entry<Mage, Integer> entry : map.entrySet()) {
            System.out.println("Key: " + entry.getKey() + ". Value: " + entry.getValue());
        }
    }

    private void get(Mage mage1){


        for(Mage mage : mage1.apprentices){
            get(mage);
            map.put(mage, mage.getAmountOfApprentice());
        }
    }

    public void printApprentices(){
        for (Mage mage : apprentices) {
            System.out.println(mage);
        }
    }
}
