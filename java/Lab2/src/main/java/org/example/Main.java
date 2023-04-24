package org.example;

import java.util.Scanner;

public class Main {

    public static void main(String[] args) {
        int numOfThreads = Integer.parseInt(args[0]); // number of threads as argument
        TaskQueue taskQueue = new TaskQueue();
        taskQueue.addTask(234122142);
        taskQueue.addTask(231);
        taskQueue.addTask(1231);
        taskQueue.addTask(2313231);
        taskQueue.addTask(2316331);
        ResultQueue resultQueue = new ResultQueue();
        Thread[] threads = new Thread[numOfThreads];
        for (int i = 0; i < numOfThreads; i++) {
            threads[i] = new Thread(new CalculationTask(taskQueue, resultQueue));
            threads[i].start();
        }

        Scanner scanner = new Scanner(System.in);
        while (true) {
            System.out.println(taskQueue.getSize());
            System.out.println("Podaj zadanie lub wpisz \"koniec\" aby zakończyć:");
            String input = scanner.nextLine();
            if (input.equals("koniec")) {
                taskQueue.setFinished();

                for (int i = 0; i < numOfThreads; i++) {
                    threads[i].interrupt();
                    taskQueue.setFinished();
                }

                break;
            }
            int number = Integer.parseInt(input);
            taskQueue.addTask(number);
        }

        for (Thread thread : threads) {
            try {
                thread.join();
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }

    }
}



