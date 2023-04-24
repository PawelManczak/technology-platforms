package org.example;

import java.util.LinkedList;
import java.util.Queue;

class ResultQueue {
    private Queue<Pair3<Integer, Integer, Double>> results = new LinkedList<>();

    public synchronized void addResult(int id, int iterations, double pi) {
        results.add(new Pair3<>(id, iterations, pi));
        printResult(id, iterations, pi);
    }

    public void printResult(int id, int iterations, double pi) {
        System.out.println("result:");

        System.out.println(id + " - " + iterations + " - " + pi);

    }

    public synchronized void printResults() {
        System.out.println("Results:");
        while (!results.isEmpty()) {
            Pair3<Integer, Integer, Double> pair = results.poll();
            System.out.println(pair.getFirst() + " - " + pair.getSecond() + " - " + pair.getThird());
        }
    }
}