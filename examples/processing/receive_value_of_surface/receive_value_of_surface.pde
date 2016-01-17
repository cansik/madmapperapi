import oscP5.*;
import netP5.*;

// OSC server and client
OscP5 osc;
NetAddress madMapper;

void setup()
{
  //init osc with default ports
  osc = new OscP5(this, 9000);
  madMapper = new NetAddress("127.0.0.1", 8000);
}

void draw()
{
}

void keyPressed()
{
  String addr = "/getControlValues?url=/surfaces/Quad 1/visible&normalized=0";
  OscMessage msg = new OscMessage(addr);
  osc.send(msg, madMapper);
}

void oscEvent(OscBundle bundle) {
  for (OscMessage m : bundle.get()) {
    // read value
    boolean isVisible = m.get(0).booleanValue();
    println("Is Visible: " + isVisible);
  }
}

void oscEvent(OscMessage msg) {
}