package org.example;

import static java.lang.Math.pow;

class CalculationTask implements Runnable {
    private TaskQueue taskQueue;
    private ResultQueue resultQueue;
    private boolean shouldRun = true;


    public CalculationTask(TaskQueue taskQueue, ResultQueue resultQueue) {
        this.taskQueue = taskQueue;
        this.resultQueue = resultQueue;

    }

    @Override
    public void run() {
        while (true) {
            Pair<Integer, Integer> taskPair = taskQueue.getTask();
            int number = taskPair.getSecond();
            if (number == -1) { // koniec pracy
                resultQueue.printResults();
                return;
            }

            double pi = 0;

            for (int i = 1; i <= number; i++) {

                double part = pow(-1, i) / ((2 * i) - 1);
                pi += part;
                if (Thread.interrupted()) {
                    resultQueue.addResult(taskPair.getFirst(), i, -4 * pi);
                    return;
                }
            }
            pi = -4 * pi;
            resultQueue.addResult(taskPair.getFirst(), number, pi);

        }
    }

}