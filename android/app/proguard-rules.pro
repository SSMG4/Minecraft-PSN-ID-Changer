# Keep VitaCheatGenerator and Region enum intact (no reflection, but
# good practice to protect serialisable data classes)
-keep class com.ssmg4.minecraftidchanger.VitaCheatGenerator { *; }
-keep enum  com.ssmg4.minecraftidchanger.Region              { *; }

# Standard Android/Kotlin rules
-keepattributes SourceFile,LineNumberTable
-renamesourcefileattribute SourceFile
