# Keep VitaCheatGenerator and Region enum intact (no reflection, but
# good practice to protect serialisable data classes)
-keep class io.github.ssmg4.mcpsn.VitaCheatGenerator { *; }
-keep enum  io.github.ssmg4.mcpsn.Region              { *; }

# Standard Android/Kotlin rules
-keepattributes SourceFile,LineNumberTable
-renamesourcefileattribute SourceFile
