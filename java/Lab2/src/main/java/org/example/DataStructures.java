package org.example;

class Pair3<T1, T2, T3> {
    private final T1 first;
    private final T2 second;
    private final T3 third;

    public Pair3(T1 first, T2 second, T3 third) {
        this.first = first;
        this.second = second;
        this.third = third;
    }

    public T1 getFirst() {
        return first;
    }

    public T2 getSecond() {
        return second;
    }

    public T3 getThird() {
        return third;
    }
}

class Pair<T1, T2> {
    private final T1 first;
    private final T2 second;


    public Pair(T1 first, T2 second) {
        this.first = first;
        this.second = second;

    }

    public T1 getFirst() {
        return first;
    }

    public T2 getSecond() {
        return second;
    }

}