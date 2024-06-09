# Mqtt-WebApp - Simplifying MQTT Communication 🚀📡

Mqtt-WebApp is a dynamic web-based application designed to streamline MQTT communication with a local broker. Developed using .NET 6 and C#, this system offers an intuitive interface for managing and interacting with MQTT messages efficiently.

## 🌟 Key Features That Enhance Your Experience

- **Seamless MQTT Integration**: Easily connect and interact with an MQTT broker directly from the web interface, simplifying message handling.

- **User-Friendly Web Interface**: The application features an intuitive UI, making it easy to publish and subscribe to MQTT topics.

- **Real-Time Messaging**: Experience real-time sending and receiving of messages, ensuring timely communication for your IoT devices.

- **Broker Configuration**: Flexibly configure your MQTT broker settings to suit your environment and requirements.

- **.NET 6 and C#**: Leveraging modern development frameworks and languages for robust and scalable performance.

## 🚀 Quick and Easy Installation

Getting Mqtt-WebApp up and running locally is straightforward. Follow these steps:

### 1. Clone the Repository

Clone the repository to your local machine using the following command:

```bash
git clone https://github.com/LeGi0N09/Mqtt-WebApp.git
cd Mqtt-WebApp
```

### 2. Install Dependencies

Ensure you have .NET 6 SDK installed. You can check your .NET version by running:

```bash
dotnet --version
```

### 3. Build the Project

Navigate to the project directory and build the project using the .NET CLI:

```bash
dotnet build
```

### 4. Run the MQTT Broker

If you want to run a local MQTT broker, you can use Docker to set up an MQTT broker like Mosquitto:

```bash
docker run -d --name mqtt-broker -p 1883:1883 eclipse-mosquitto
```

### 5. Configure the Application

Update the `appsettings.json` file in the project to configure the MQTT broker settings. Ensure the broker address, port, and other settings match your MQTT broker setup.

### 6. Run the Application

Run the application using the .NET CLI:

```bash
dotnet run
```

Alternatively, you can run the application from Visual Studio by opening the solution file and starting the project.

### 7. Access the Web Interface

Once the application is running, open your web browser and navigate to `http://localhost:5000` (or the appropriate port if you have configured a different one).

## 🎯 How to Use Mqtt-WebApp

Experience Mqtt-WebApp's effortless usage:

### 1. Homepage

- View available MQTT topics for interaction.
- Use the navigation bar to access different sections of the application.

### 2. Publish Messages

- Navigate to the "Publish" section.
- Enter the topic and message payload.
- Click the "Publish" button to send the message to the MQTT broker.

### 3. Subscribe to Topics

- Navigate to the "Subscribe" section.
- Enter the topic you want to subscribe to.
- Click the "Subscribe" button to start receiving messages from the MQTT broker.

## 🛠️ Technologies That Power Mqtt-WebApp

Mqtt-WebApp utilizes the following technologies to ensure a robust and efficient experience:

- .NET 6
- C#
- Docker (for optional MQTT broker setup)

## 📝 License

Mqtt-WebApp operates under the [MIT License](LICENSE).

## 🤝 Contributing

We welcome contributions, issues, and feature requests! Check out our [contribution guidelines](CONTRIBUTING.md) to get involved.

## 👤 Author

Created by [Priyansh Modi](https://github.com/LeGi0N09).

## 📄 Additional Documentation

For more details and information, refer to the [documentation](docs/README.md).


Thank you for choosing Mqtt-WebApp! Streamline your MQTT communication and optimize your messaging workflows. Should you have any inquiries or require assistance, don't hesitate to reach out.

Happy messaging! 🚀📡

