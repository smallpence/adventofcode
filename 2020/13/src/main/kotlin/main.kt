import java.io.File

fun main(args: Array<String>) {
    val input = File("input").readLines()

    val departTime = input[0].toInt()

    val buses = input[1]
        .split(",")
        .filter { line -> line != "x" }

    val nexts = buses
        .map { line -> line.toInt() }
        .map { interval -> interval * ((departTime / interval) + 1) }

    val next = nexts.minOrNull()
    val index = nexts.indexOf(next)

    if (next != null) {
        val wait = next - departTime
        println(wait * buses[index].toInt())
    }
}