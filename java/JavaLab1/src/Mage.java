import java.util.HashSet;
import java.util.Objects;
import java.util.Set;

public class Mage implements Comparable<Mage> {
    private String name;
    private int level;
    private double power;
    private Set<Mage> apprentices = new HashSet<>();

    public Mage(String name, int level, double power) {
         this.name = name;
         this.level = level;
         this.power = power;

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

    public void addApprentice(Mage mag) {
        this.apprentices.add(mag);
    }

    public double getPower() {
        return power;
    }

    public int getLevel() {
        return level;
    }
}
