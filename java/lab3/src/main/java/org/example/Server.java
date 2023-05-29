package org.example;

import java.io.*;
import java.net.*;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

public class Server {
    private static final int PORT = 12234;

    public static void main(String[] args) {
        ExecutorService executorService = Executors.newCachedThreadPool();

        try (ServerSocket serverSocket = new ServerSocket(PORT)) {
            System.out.println("Server started on port " + PORT);

            while (true) {
                Socket clientSocket = serverSocket.accept();
                System.out.println("New client connected: " + clientSocket.getInetAddress().getHostAddress());

                // create a new client handler thread to handle the client connection
                ClientHandler clientHandler = new ClientHandler(clientSocket);
                executorService.submit(clientHandler);
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private static class ClientHandler implements Runnable {
        private final Socket clientSocket;

        public ClientHandler(Socket clientSocket) {
            this.clientSocket = clientSocket;
        }

        @Override
        public void run() {
            try (ObjectInputStream objectInputStream = new ObjectInputStream(clientSocket.getInputStream());
                 ObjectOutputStream objectOutputStream = new ObjectOutputStream(clientSocket.getOutputStream())) {

                Message initialMessage = new Message(0, "connected!");

                // send the response message object to the client
                objectOutputStream.writeObject(initialMessage);
                objectOutputStream.flush();

                Message numberOfMessages = (Message) objectInputStream.readObject();
                int N = numberOfMessages.getNumber();
                System.out.println("Number of messages: " + N);


                for(int i = 0 ; i< N; i++) {
                    // read the incoming message object from the client
                    Message message = (Message) objectInputStream.readObject();
                    System.out.println("Message received from client: " + message);

                    // create a response message
                    Message responseMessage = new Message(message.getNumber(), message.getContent());

                    // send the response message object to the client
                    objectOutputStream.writeObject(responseMessage);
                    objectOutputStream.flush();
                }

                Message responseMessage = new Message(N, "connection closed");
                objectOutputStream.writeObject(responseMessage);
                objectOutputStream.flush();

            } catch (IOException | ClassNotFoundException e) {
                System.out.println("Client disconnected: " + clientSocket.getInetAddress().getHostAddress());
            }
        }
    }
}
