package org.example;

import java.io.*;
import java.net.*;
import java.util.*;

// Client class
class Client {

    public static void main(String[] args)
    {
        // establish a connection by providing host and port number
        try (Socket socket = new Socket("localhost", 12234)) {

            // writing to server
            ObjectOutputStream out = new ObjectOutputStream(socket.getOutputStream());

            // reading from server
            ObjectInputStream in = new ObjectInputStream(socket.getInputStream());

            // object of scanner class
            Scanner sc = new Scanner(System.in);
            String line = null;
            System.out.println("Server replied: " + in.readObject());

            System.out.println("Enter number of messages:");
            int numberOfMessages = sc.nextInt();
            Message initialMessage = new Message(numberOfMessages, "N");
            out.writeObject(initialMessage);
            out.flush();
            Message response = new Message(-1, "");

            for(int i = 0 ; i< numberOfMessages; i++) {

                // reading from user
                System.out.println("Enter message number:");
                int number = sc.nextInt();
                sc.nextLine(); // consume newline character
                System.out.println("Enter message content:");
                String content = sc.nextLine();

                // creating a message object with user input
                Message message = new Message(number, content);

                // sending the message object to server
                out.writeObject(message);
                out.flush();
                response = (Message) in.readObject();
                // displaying server reply
                System.out.println("Server replied: " + response);
            }
            response = (Message) in.readObject();
            // displaying server reply
            System.out.println("Server replied: " + response);
            // closing the scanner object
            sc.close();
        }
        catch (IOException | ClassNotFoundException e) {
            e.printStackTrace();
        }
    }
}
