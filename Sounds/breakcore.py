from pyo import *
from random import randint
import os
from typing import Union
import math






class BreakCore:
    def __init__(self):
        self.sounds = [os.path.join("drums", sound) for sound in os.listdir("drums")]
        self.r_side = True
        self.stereo = True
        self.lfo = LFO(freq=0.5, mul=2, type=3)
        self.sf = SfPlayer(self.sounds[0], speed=self.lfo, loop=False, mul=0.4).out(1)
        self.trig = None
        self.pat = Pattern(self.new, time=.1)

    def new(self):

        chance = randint(0, 3)
        if chance == 0:
            self.stereo = not self.stereo

        elif chance == 1:
            self.r_side = not self.r_side

        self.sf.path = self.sounds[randint(0, len(self.sounds) - 1)]
        self.sf.out(int(self.r_side), int(self.stereo))

    def set_trig(self, trig: PyoObject):
        self.trig = TrigFunc(trig, function=self.new)

    def ctrl(self):
        self.sf.ctrl()
        self.lfo.ctrl()

    def p(self):
        self.pat.play()

    def ext_ctrl(self, address, *args):
        print(address)
        freq = 10 ** (args[0] - 2) # 1 TO 6
        sharp = args[1] # 0 to 1
        wav_type = int(args[2]) # 0 to 7
        pat_time = args[3] # 0.050 to 1

        self.lfo.freq = freq
        self.lfo.sharp = sharp
        self.lfo.type = wav_type
        self.pat.time = pat_time

        print(f"New OSC: freq {freq}, sharp {sharp}, wav_type {wav_type}, pat_time {pat_time}")




if __name__ == "__main__":

    s = Server().boot()

    br = BreakCore()
    br.ctrl()
    path = "rec.wav"
    # Record for 10 seconds a 24-bit wav file.
    s.recordOptions(filename=path)
    br.p()

    r = OscDataReceive(12000, "/wek/outputs", br.ext_ctrl)





    s.gui(locals())