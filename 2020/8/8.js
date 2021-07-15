"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var fs_1 = require("fs");
var Instruction = /** @class */ (function () {
    function Instruction(opcode, operand, run) {
        if (run === void 0) { run = false; }
        this.opcode = opcode;
        this.operand = operand;
        this.run = run;
    }
    return Instruction;
}());
var Machine = /** @class */ (function () {
    function Machine(instructions) {
        this.accumulator = 0;
        this.programCounter = -1;
        this.instructions = instructions;
    }
    Machine.prototype.execInstruction = function () {
        var currentInstruction = this.instructions[++this.programCounter];
        console.log(currentInstruction);
        if (currentInstruction.run)
            return false;
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
    };
    Machine.prototype.exec = function (initialValue) {
        this.accumulator = initialValue;
        while (this.execInstruction())
            ;
        return this.accumulator;
    };
    return Machine;
}());
var instructions = fs_1.readFileSync("input", "utf-8")
    .split("\n")
    .filter(function (line) { return !!line; })
    .map(function (line) { return line.split(" "); })
    .map(function (lineParts) { return new Instruction(lineParts[0], lineParts[1].includes("+") ? parseInt(lineParts[1].replace("+", "")) : parseInt(lineParts[1])); });
var machine = new Machine(instructions);
console.log(machine.exec(0));
