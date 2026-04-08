package com.ssmg4.minecraftidchanger

import org.junit.Assert.*
import org.junit.Test

class VitaCheatGeneratorTest {

    @Test
    fun `generate uses correct base address`() {
        val result = VitaCheatGenerator.generate("AAAA")
        // "AAAA" → 0x41414141 → reversed little-endian → still 41414141
        assertTrue(result.content.contains("8234628D"))
    }

    @Test
    fun `4-char ASCII name produces exactly two lines plus header`() {
        val result = VitaCheatGenerator.generate("AAAA", "Test")
        val lines = result.content.lines().filter { it.isNotBlank() }
        // _V0 line + 1 data chunk + 1 null-terminator = 3 lines
        assertEquals(3, lines.size)
    }

    @Test
    fun `8-char name produces three lines plus header`() {
        val result = VitaCheatGenerator.generate("AAAABBBB", "Test")
        val lines = result.content.lines().filter { it.isNotBlank() }
        // _V0 + 2 data chunks + null-terminator = 4 lines
        assertEquals(4, lines.size)
    }

    @Test
    fun `null terminator address is correctly offset`() {
        // "AAAA" is 4 bytes → aligned length = 4 → null at BASE + 4 = 0x82346291
        val result = VitaCheatGenerator.generate("AAAA")
        assertTrue(result.content.contains("82346291 00000000"))
    }

    @Test
    fun `null terminator for 5-char name is at +8`() {
        // 5 bytes → aligned to 8 → null at BASE + 8 = 0x82346295
        val result = VitaCheatGenerator.generate("AAAAB")
        assertTrue(result.content.contains("82346295 00000000"))
    }

    @Test
    fun `blank code name defaults to NewID`() {
        val result = VitaCheatGenerator.generate("Test", "   ")
        assertTrue(result.content.startsWith("_V0 NewID"))
        assertEquals("NewID", result.codeName)
    }

    @Test
    fun `little endian encoding is correct for known value`() {
        // "ABCD" → bytes 0x41 0x42 0x43 0x44 → reversed → 44434241
        val result = VitaCheatGenerator.generate("ABCD")
        assertTrue(result.content.contains("44434241"))
    }

    @Test
    fun `UTF-8 section symbol is encoded`() {
        // § is 0xC2 0xA7 in UTF-8
        val result = VitaCheatGenerator.generate("§")
        // bytes: C2 A7 → padded to C2 A7 00 00 → reversed → 0000A7C2
        assertTrue(result.content.contains("0000A7C2"))
    }

    @Test
    fun `PSN_MIN_LENGTH and PSN_MAX_LENGTH constants are correct`() {
        assertEquals(3,  VitaCheatGenerator.PSN_MIN_LENGTH)
        assertEquals(16, VitaCheatGenerator.PSN_MAX_LENGTH)
    }
}
