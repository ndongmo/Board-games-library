# Board games library
**tgLib** is a lightweight Client-Server C# library for board games. It is modular and very easy to use. It provides functionalities for server and client which can be used independently. The code is provided with server and client application examples, and with tgLib class diagram for good understanding how it works. The client application implements the well-known **Connect4** game. You can have a look at the video gameplay on my [youtube channel](https://youtu.be/GuC5tC1gA5Y).

[![Gameplay video on youtube](https://j.gifs.com/VAmZGW.gif)](https://youtu.be/GuC5tC1gA5Y)
  
  ## Features
  
  - Sever can run in background mode or window mode
  - Data management using file or database
  - Player data management (registration, login, score)
  - Concurrency through multithreading (one thread per client)
  
  ## Requirements
  
  The code has been compiled with visual studio 2013 (.Net Framework 4.5) without any special package or library. Although I didn't test     it with another version of visual studio, I assume it should work for any version of visual studio let's say greater or equal to 2010.       Also, I use Microsoft.ACE.OLEDB.12.0 as provider in the server application example. So you will need to install it if you intend to use   the same provider or you can just use another .Net provider.
  
  ## Architecture
  
  The library is designed in two modules : the client module which implements client functionalities and the server module for server     functionalities. Each module follows MVC architecture.
  
  <img src="https://github.com/ndongmo/Board-games-library/blob/master/images/project_architecture.PNG" align="left" width="25%" height="400"      alt="Project architecture">
  <img src="https://github.com/ndongmo/Board-games-library/blob/master/images/tgLib_Class_diagram.jpg" align="right" width="70%" height="400"      alt="tgLib class diagram">
  
  ## How it works
  
  **tgLib** is very simple, the view of the server application should implement the **IServerView** interface of tgLib and that's all     for the server side :relaxed:. On the other side, the client application game class should inherit from the **Game** class of tgLib and   the view should also implement the **IClientView** interface of tgLib. The diagrams below show how the server and client examples       have been designed. It is a good example of how to use tgLib for any board game.
  
  <table style="width:100%">
  <tr>
    <td>
      <img src="https://github.com/ndongmo/Board-games-library/blob/master/images/ServerApp_Class_diagram.PNG" alt="Server App class diagram"/>
    </td>
    <td>
     <img src="https://github.com/ndongmo/Board-games-library/blob/master/images/ClientApp_Class_diagram.PNG" alt="Client App class diagram" />
    </td>
    <td>
      <img src="https://github.com/ndongmo/Board-games-library/blob/master/images/Client_stateMachine_diagram.jpg" alt="Client state machine           diagram"/>
    </td>
  </tr>
 </table>

## Running the examples
You can find in the [readyToUse](https://github.com/ndongmo/Board-games-library/blob/master/readyToUse) directory the client EXE and the server EXE in which I embedded the tgLib.dll for convenience thanks to [Fody](https://github.com/Fody/Costura) package. Unfortunatelly, I couldn't do the same with the user DB (access db). I run my tests on the same machine, so please do not hesitate to raise an issue.

Once the server has started, the client can connect and logged if he has already registered. Otherwise, he can register with by filling his login and password, or he can stay as anonymous player. Of course as anonymous player he have no score history.

<table style="width:100%">
  <tr>
    <td>
      <img src="https://github.com/ndongmo/Board-games-library/blob/master/images/server_1.PNG" />
    </td>
    <td>
      <img src="https://github.com/ndongmo/Board-games-library/blob/master/images/client_1_1.PNG" />
    </td>
    <td>
     <img src="https://github.com/ndongmo/Board-games-library/blob/master/images/client_2_1.PNG" />
    </td>
    <td>
      <img src="https://github.com/ndongmo/Board-games-library/blob/master/images/client_3_1.PNG" />
    </td>
  </tr>
 </table>
 
 Once the client is connected or logged, he can see the list of connected players and choose the player he want to challenge. The selected player on his side receives the challenge request and can reject or accept it. If the request is accepted, the both players can begin the game and cannot receive any other request while playing. During a game, the challengers can communicate through a chat box on the right panel. Moreover, any challenger can abort the game at any time.
 
<table style="width:100%">
  <tr>
    <td>
      <img src="https://github.com/ndongmo/Board-games-library/blob/master/images/server_1.PNG" />
    </td>
    <td>
      <img src="https://github.com/ndongmo/Board-games-library/blob/master/images/client_1_2.PNG" />
    </td>
    <td>
     <img src="https://github.com/ndongmo/Board-games-library/blob/master/images/client_2_2.PNG" />
    </td>
    <td>
      <img src="https://github.com/ndongmo/Board-games-library/blob/master/images/client_3_1.PNG" />
    </td>
  </tr>
  <tr>
    <td>
      <img src="https://github.com/ndongmo/Board-games-library/blob/master/images/server_2.PNG" />
    </td>
    <td>
      <img src="https://github.com/ndongmo/Board-games-library/blob/master/images/client_1_4.PNG" />
    </td>
    <td>
     <img src="https://github.com/ndongmo/Board-games-library/blob/master/images/client_2_4.PNG" />
    </td>
    <td>
      <img src="https://github.com/ndongmo/Board-games-library/blob/master/images/client_3_3.PNG" />
    </td>
  </tr>
  <tr>
    <td>
      <img src="https://github.com/ndongmo/Board-games-library/blob/master/images/server_2.PNG" />
    </td>
    <td>
      <img src="https://github.com/ndongmo/Board-games-library/blob/master/images/client_1_5.PNG" />
    </td>
    <td>
     <img src="https://github.com/ndongmo/Board-games-library/blob/master/images/client_2_5.PNG" />
    </td>
    <td>
      <img src="https://github.com/ndongmo/Board-games-library/blob/master/images/client_3_3.PNG" />
    </td>
  </tr>
   <tr>
    <td>
      <img src="https://github.com/ndongmo/Board-games-library/blob/master/images/server_2.PNG" />
    </td>
    <td>
      <img src="https://github.com/ndongmo/Board-games-library/blob/master/images/client_1_6.PNG" />
    </td>
    <td>
     <img src="https://github.com/ndongmo/Board-games-library/blob/master/images/client_2_6.PNG" />
    </td>
    <td>
      <img src="https://github.com/ndongmo/Board-games-library/blob/master/images/client_3_3.PNG" />
    </td>
  </tr>
 </table>
 
## Authors

* F. Ndongmo Silatsa

## Licence

This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/ndongmo/Board-games-library/blob/master/LICENSE.md) file for details

## Acknowledgments

* [Dominikus Herzberg](https://github.com/ndongmo/BitboardC4/blob/master/BitboardDesign.md) for his explantions of the Connect4 algorithm.
* [qu1j0t3](https://github.com/qu1j0t3/fhourstones) wo has implemented an efficient Connect4 algorithm in Java.

 
