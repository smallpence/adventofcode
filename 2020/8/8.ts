import { readFileSync } from 'fs';

class Instruction {
    constructor(readonly opcode:string, readonly operand:number, public run:boolean = false) {}
}

class Machine {
    instructions: Instruction[];
    accumulator = 0;
    programCounter = -1;

    constructor(instructions: Instruction[]) {
        this.instructions = instructions;
    }

    private execInstruction():boolean {
        const currentInstruction = this.instructions[++this.programCounter];
        console.log(currentInstruction);
        if (currentInstruction.run) return false;

        switch (currentInstruction.opcode) {
            case "acc":
                this.accumulator += currentInstruction.operand;
                break;

            case "jmp":
                this.programCounter += currentInstruction.operand - 1;
                break;
        }

        currentInstruction.run = true;
        return true;
    }

    public exec(initialValue: number):number {
        this.accumulator = initialValue;
        while (this.execInstruction());
        return this.accumulator;
    }
}

const instructions = readFileSync("input","utf-8")
    .split("\n")
    .filter(line => !!line)
    .map(line => line.split(" "))
    .map(lineParts => new Instruction(lineParts[0], lineParts[1].includes("+") ? parseInt(lineParts[1].replace("+","")) : parseInt(lineParts[1])));

const machine = new Machine(instructions);

console.log(machine.exec(0));