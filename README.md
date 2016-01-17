# MadMapper OSC API
This is a documentation and example respsitory for the [MadMapper][1] OSC API which was released with the version **2.2** in December 2015.

The goal of this repository is to gather examples and documentations for the api together to give other developers a quick start and and overview over the api.

## Quickstart

1. Install an OSC library
2. Create a new OSC client and server
3. Communicate with MadMapper

## Introduction
The MadMapper OSC API uses OSC to send and receive messages. So it is useful to understand OSC and it's possibilities.

### OSC
[OSC][4] is a simple [UDP][6] based network protocol between a server and multiple clients. UDP gives you the ability to just send data and don't care about, if the client is listening or not or how many client's are listening to your message. 

The benefit of this is that it is really fast and simple. The drawback of this is that if you want to create a bidirectional channel between your software and and antoher, you have to open a server and a client on each side.

Over OSC you can send **messages** which contain a specific **address**. This address will be recognized by a client and will cause it to do something. As an addition it is also possible to send data within the message to for examples update a parameter.

![OSC Architecture](images/osc_architecture.png)
*Simplified OSC Architecture*

#### OSC Bundles
To put multiple messages together it is also possible to create so called **bundles**. These contain a list of OSC messages which then will be sent over the network.
So a client is able to get messages which are related to each other as a bundle.

#### OSC Libraries
To use OSC it is recommended to use a library which covers the basic OSC methods. Here are listet the one's that are used in the examples.

##### Processing (Java)
* [oscp5][12]
	* [Documentation][13]
	* License: Copyright 2004-2015 Andreas Schlegel
	* It's recommended to use the **latest release** of the library from github!

##### .NET (C#)
* [VVVV OSC Library][9]
	* [Documentation][10]
	* License: [GPLv2][11]

## API Documentation

### Overview

![MadMapper OSC API Architecture](images/mm_architecture.png)
*MadMapper OSC API Architecture*

### getControls
The Method `getControls` is to receive the available controls from MadMapper.

```
/getControls?root=ROOT_URL&recursive=RECURSIVE
```

#### Parameters 
* `ROOT_URL` (*String*): The node from where to search for other controls. 
* `RECURSIVE` (*Boolean*): Defines if MadMapper should send only the direct children of the `ROOT_URL` control, or if it should send back all controls below this control. Value: **0 or 1**

#### Response
MadMapper replies with a bundle which contains a message for each child of the requested node on the address of the node.

#### Example
If there is one surface in MadMapper called "Quad 1" and you send a message to following address, MadMapper sends back a bundle with two messages without data on this addresses.

**Request**

```
/getControls?root=/surfaces&recursive=0
```

**Response**

```
/surfaces/selected
/surfaces/Quad 1
```

### getControlValues


#### Coordinate System

## Examples
All the basic exampels are written in [processing][5] to show the general idea behind it. They should be easaly be portable to other languages and just give an idea how the API works.

### First Steps

### Send Data to MadMapper

### Receive Data from MadMapper

## Issues
Currently the API is very young so there may be some bugs. If you find one please open a new issue and describe it as good as possible and how to reproduce it.

## Contribute
If you would like to share your api project or extend an example or framework please create a pull request or just create a new issue.

## About

MadMapper has been developed by [GarageCUBE][2].

This page is maintained by [Florian 'cansik' Bruggisser][7].

 [1]: http://www.madmapper.com/
 [2]: http://www.garagecube.com/
 [3]: http://www.modul8.ch/
 [4]: https://de.wikipedia.org/wiki/Open_Sound_Control
 [5]: https://processing.org/
 [6]: https://wikipedia.org/wiki/User_Datagram_Protocol
 [7]: https://github.com/cansik
 [8]: https://github.com/sojamo/oscp5
 [9]: https://github.com/vvvv/vvvv-sdk/tree/develop/common/src/core/Utils/OSC
 [10]: https://vvvv.org/documentation/osc
 [11]: https://www.gnu.org/licenses/gpl-2.0.html
 [12]: https://github.com/sojamo/oscp5
 [13]: http://www.sojamo.de/libraries/oscP5/