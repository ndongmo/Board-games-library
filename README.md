# Board games library
**tgLib** is a lightweight Client-Server .Net library for board games. It is modulable and very easy to use. It provides functionalities for server and client which can be used independently. The code is provided with server and client application examples, and wiht tgLib class diagram for good understanding how it works. The client application provided implements the well-known **Connect4** game.
  
  ## Features
  
  - Sever can run in background mode or window mode
  - Data management via file or database
  - Player data management (registration, login, score)
  - Concurency through multithreading (one thread per client)
  
  ## Requirements
  
  The code has been compiled with visual studio 2013 (.Net Framework 4.5) without any special package or library. Although I didn't test     it with other version of visual studio, I assume it should work for any version of visual studio let's say greater or equal to 2010.       Also, I use Microsoft.ACE.OLEDB.12.0 as provider in the server application example. So you will need to install it if you intend to use   the same provider or you can just use another .Net provider.
  
  ## Architecture
  
  The library is design in two modules : the client module which implements client functionalities and the server module for server         functionalities. Each module follows MVC architecture.
  
  <img src="https://github.com/ndongmo/Board-games-library/blob/master/project_architecture.PNG" align="left" width="25%" height="400"      alt="Project architecture">
  <img src="https://github.com/ndongmo/Board-games-library/blob/master/tgLib_Class_diagram.jpg" align="right" width="70%" height="400"      alt="tgLib class diagram">
  
  
  
  
  
  ## How it works
  
  **tgLib** is very simple, the view of the server application should implement the **IServerView** interface of tgLib and that's all     for the server side :relaxed:. On the other side, the client application game class should inherit of the **Game** class of tgLib and   the view should also implement the **IClientView** interface of tgLib. The diagrams bellow show how the server and client examples       have been designed. It is a good example of how to use tgLib for any board game.
  
  <table style="width:100%">
  <tr>
    <td>
      <img src="https://github.com/ndongmo/Board-games-library/blob/master/ServerApp_Class_diagram.PNG" alt="Server App class diagram"/>
    </td>
    <td>
     <img src="https://github.com/ndongmo/Board-games-library/blob/master/ClientApp_Class_diagram.PNG" alt="Client App class diagram" />
    </td>
    <td>
      <img src="https://github.com/ndongmo/Board-games-library/blob/master/Client_stateMachine_diagram.jpg" alt="Client state machine           diagram"/>
    </td>
  </tr>
 </table>

  
 
