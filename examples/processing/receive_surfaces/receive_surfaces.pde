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
  osc.send(new OscMessage("/getControls?root=/surfaces&recursive=0"), madMapper);
}

void oscEvent(OscBundle bundle) {
  println("bundle received!");
  for (OscMessage m : bundle.get()) {
    print(m.getAddress());
  }
}

void oscEvent(OscMessage msg) {
}