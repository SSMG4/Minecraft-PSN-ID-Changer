package com.ssmg4.minecraftidchanger

/**
 * Pure-Kotlin port of the Windows VitaCheat code generator.
 *
 * Encodes [username] to UTF-8, writes it in 4-byte little-endian chunks
 * starting at [BASE_ADDRESS], then appends a null-terminator block so
 * any previously longer name is fully cleared.
 */
object VitaCheatGenerator {

    // Static memory address for the Minecraft PS Vita username string
    private const val BASE_ADDRESS: Long = 0x8234628DL

    /** PSN ID limits (3–16 chars). Soft-enforced; generator still runs above 16. */
    const val PSN_MIN_LENGTH = 3
    const val PSN_MAX_LENGTH = 16

    data class Result(
        val codeName: String,
        val content: String,
    )

    /**
     * @param username  The desired in-game name (UTF-8 encoded).
     * @param codeName  Label written on the _V0 line; defaults to "NewID".
     * @param region    One of the [Region] entries; used only for display,
     *                  the memory address is the same for all regions.
     */
    fun generate(
        username: String,
        codeName: String = "NewID",
        @Suppress("UNUSED_PARAMETER") region: Region = Region.EU,
    ): Result {
        val effectiveName = codeName.trim().ifBlank { "NewID" }
        val bytes = username.toByteArray(Charsets.UTF_8)

        val sb = StringBuilder()
        sb.appendLine("_V0 $effectiveName")

        // Process in 4-byte little-endian chunks
        var offset = 0
        while (offset < bytes.size) {
            val chunk = ByteArray(4) // zero-initialised → auto-padded
            val count = minOf(4, bytes.size - offset)
            bytes.copyInto(chunk, destinationOffset = 0, startIndex = offset, endIndex = offset + count)

            chunk.reverse() // PS Vita is little-endian
            val hex = chunk.toHex()

            val address = (BASE_ADDRESS + offset) and 0xFFFFFFFFL
            sb.appendLine("\$0200 ${address.toAddressHex()} $hex")
            offset += 4
        }

        // Null-terminator: one 4-byte zero block at the next aligned address.
        // Ensures any previously written longer name is fully overwritten.
        val alignedLen = ((bytes.size + 3) / 4) * 4
        val nullAddress = (BASE_ADDRESS + alignedLen) and 0xFFFFFFFFL
        sb.appendLine("\$0200 ${nullAddress.toAddressHex()} 00000000")

        return Result(
            codeName = effectiveName,
            content  = sb.toString().trimEnd(),
        )
    }

    // ── Private helpers ────────────────────────────────────────────────────

    private fun ByteArray.toHex(): String =
        joinToString("") { "%02X".format(it) }

    private fun Long.toAddressHex(): String =
        "%08X".format(this)
}

/** Supported Minecraft PS Vita game regions with their save-file names. */
enum class Region(val fileName: String, val displayName: String) {
    EU("PCSB00560.psv", "Europe (PCSB00560)"),
    US("PCSE00491.psv", "Americas (PCSE00491)"),
    JP("PCSG00302.psv", "Japan (PCSG00302)"),
}
