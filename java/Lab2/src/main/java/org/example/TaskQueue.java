package org.example;

import java.util.LinkedList;
import java.util.Queue;

class TaskQueue {
    private static Integer id = 1;
    private Queue<Pair<Integer, Integer>> tasks = new LinkedList<>();
    private boolean finished = false;
    private boolean shouldStop = false;
    public synchronized void addTask(int number) {
        tasks.add(new Pair<>(id, number));
        id++;
        notifyAll();
    }

    public synchronized Pair<Integer, Integer> getTask() {
        while (tasks.isEmpty() && !finished) {
            try {
                wait(); // oczekiwanie na zadania lub koniec pracy
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
        if (tasks.isEmpty()) {
            return new Pair<>(-1, -1); // zwracanie -1 oznacza koniec pracy
        }
        return tasks.poll();
    }

    public synchronized void setFinished() {
        finished = true;
        notifyAll();
    }

    public boolean isEmpty(){
        return tasks.isEmpty();
    }

    public int getSize(){
        return tasks.size();
    }

    public boolean getShouldStop(){
        return shouldStop;
    }

}
