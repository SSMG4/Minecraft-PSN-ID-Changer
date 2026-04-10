package io.github.ssmg4.mcpsn

import android.content.ClipData
import android.content.ClipboardManager
import android.content.Context
import android.content.Intent
import android.net.Uri
import android.os.Bundle
import android.text.Editable
import android.text.TextWatcher
import android.view.Menu
import android.view.MenuItem
import android.widget.ArrayAdapter
import android.widget.Toast
import androidx.activity.result.contract.ActivityResultContracts
import androidx.appcompat.app.AlertDialog
import androidx.appcompat.app.AppCompatActivity
import androidx.core.content.ContextCompat
import androidx.core.view.isVisible
import com.google.android.material.color.MaterialColors
import io.github.ssmg4.mcpsn.databinding.ActivityMainBinding
import java.io.IOException

class MainActivity : AppCompatActivity() {

    private lateinit var binding: ActivityMainBinding

    /** Last generated code content; null until Generate is pressed. */
    private var lastGeneratedContent: String? = null

    // SAF file-save launcher
    private val saveLauncher =
        registerForActivityResult(ActivityResultContracts.CreateDocument("*/*")) { uri ->
            uri?.let { writeContentToUri(it) }
        }

    // ── Lifecycle ──────────────────────────────────────────────────────────

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)
        setSupportActionBar(binding.toolbar)

        setupRegionSpinner()
        setupUsernameCounter()
        setupButtons()
    }

    override fun onCreateOptionsMenu(menu: Menu): Boolean {
        menuInflater.inflate(R.menu.menu_main, menu)
        return true
    }

    override fun onOptionsItemSelected(item: MenuItem): Boolean =
        when (item.itemId) {
            R.id.action_clear -> { clearAll(); true }
            R.id.action_about -> { showAboutDialog(); true }
            else              -> super.onOptionsItemSelected(item)
        }

    // ── Setup helpers ──────────────────────────────────────────────────────

    private fun setupRegionSpinner() {
        val adapter = ArrayAdapter(
            this,
            android.R.layout.simple_spinner_item,
            Region.entries.map { it.displayName },
        ).also { it.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item) }
        binding.spinnerRegion.adapter = adapter
    }

    private fun setupUsernameCounter() {
        binding.editUsername.addTextChangedListener(object : TextWatcher {
            override fun beforeTextChanged(s: CharSequence?, start: Int, count: Int, after: Int) = Unit
            override fun onTextChanged(s: CharSequence?, start: Int, before: Int, count: Int) = Unit
            override fun afterTextChanged(s: Editable?) {
                val len = s?.length ?: 0
                val max = VitaCheatGenerator.PSN_MAX_LENGTH
                binding.textInputUsername.counterMaxLength = max
                // Tint the counter red when over limit
                val overLimit = len > max
                binding.textInputUsername.isCounterEnabled = true
                if (overLimit) {
                    binding.textInputUsername.error = getString(R.string.warn_over_limit, max)
                } else {
                    binding.textInputUsername.error = null
                }
            }
        })
    }

    private fun setupButtons() {
        binding.btnGenerate.setOnClickListener { onGenerateClicked() }
        binding.btnCopy.setOnClickListener    { onCopyClicked() }
        binding.btnShare.setOnClickListener   { onShareClicked() }
        binding.btnSave.setOnClickListener    { onSaveClicked() }
    }

    // ── Button handlers ────────────────────────────────────────────────────

    private fun onGenerateClicked() {
        val username = binding.editUsername.text?.toString().orEmpty().trim()
        val codeName = binding.editCodeName.text?.toString().orEmpty().trim()
        val region   = Region.entries[binding.spinnerRegion.selectedItemPosition]

        if (username.length < VitaCheatGenerator.PSN_MIN_LENGTH) {
            binding.textInputUsername.error =
                getString(R.string.err_too_short, VitaCheatGenerator.PSN_MIN_LENGTH)
            return
        }

        val proceedWithGenerate = {
            val result = VitaCheatGenerator.generate(username, codeName, region)
            lastGeneratedContent = result.content
            binding.textOutput.setText(result.content)
            binding.cardOutput.isVisible = true
            binding.btnCopy.isEnabled  = true
            binding.btnShare.isEnabled = true
            binding.btnSave.isEnabled  = true
        }

        if (username.length > VitaCheatGenerator.PSN_MAX_LENGTH) {
            AlertDialog.Builder(this)
                .setTitle(R.string.dlg_length_title)
                .setMessage(getString(R.string.dlg_length_body, VitaCheatGenerator.PSN_MAX_LENGTH))
                .setPositiveButton(R.string.continue_anyway) { _, _ -> proceedWithGenerate() }
                .setNegativeButton(android.R.string.cancel, null)
                .show()
        } else {
            proceedWithGenerate()
        }
    }

    private fun onCopyClicked() {
        val content = lastGeneratedContent ?: return
        val clipboard = getSystemService(Context.CLIPBOARD_SERVICE) as ClipboardManager
        clipboard.setPrimaryClip(ClipData.newPlainText("VitaCheat Code", content))
        toast(R.string.toast_copied)
    }

    private fun onShareClicked() {
        val content = lastGeneratedContent ?: return
        val intent = Intent(Intent.ACTION_SEND).apply {
            type = "text/plain"
            putExtra(Intent.EXTRA_TEXT, content)
            putExtra(Intent.EXTRA_SUBJECT, getString(R.string.share_subject))
        }
        startActivity(Intent.createChooser(intent, getString(R.string.share_via)))
    }

    private fun onSaveClicked() {
        val region = Region.entries[binding.spinnerRegion.selectedItemPosition]
        saveLauncher.launch(region.fileName)
    }

    private fun clearAll() {
        binding.editUsername.setText("")
        binding.editCodeName.setText("")
        binding.textOutput.setText("")
        lastGeneratedContent = null
        binding.cardOutput.isVisible = false
        binding.btnCopy.isEnabled  = false
        binding.btnShare.isEnabled = false
        binding.btnSave.isEnabled  = false
    }

    // ── File I/O ───────────────────────────────────────────────────────────

    private fun writeContentToUri(uri: Uri) {
        val content = lastGeneratedContent ?: return
        try {
            contentResolver.openOutputStream(uri)?.use { out ->
                out.writer(Charsets.UTF_8).use { it.write(content) }
            }
            toast(R.string.toast_saved)
        } catch (e: IOException) {
            AlertDialog.Builder(this)
                .setTitle(R.string.err_save_title)
                .setMessage(e.localizedMessage)
                .setPositiveButton(android.R.string.ok, null)
                .show()
        }
    }

    // ── Misc ───────────────────────────────────────────────────────────────

    private fun showAboutDialog() {
        AlertDialog.Builder(this)
            .setTitle(R.string.about_title)
            .setMessage(R.string.about_body)
            .setPositiveButton(android.R.string.ok, null)
            .show()
    }

    private fun toast(resId: Int) =
        Toast.makeText(this, resId, Toast.LENGTH_SHORT).show()
}
