using System.Windows.Forms;

namespace TheLeftExit.TeslaX.Static
{
    internal static class Message
    {
        public static void NoWindow() =>
            MessageBox.Show("Growtopia isn't open.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void NoNewOffset() =>
            MessageBox.Show("Failed to generate the block grid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void NoNewPlayer() =>
            MessageBox.Show("Failed to find the player.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void NoCustomSpritesheet() =>
            MessageBox.Show("Invalid spritesheet file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void TextureDeleted() =>
            MessageBox.Show("Textures in /game deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        public static void TextureAlreadyDeleted() =>
            MessageBox.Show("Textures in /game not found.", "No action required", MessageBoxButtons.OK, MessageBoxIcon.Information);

        public static void TextureSwapped() =>
            MessageBox.Show("Textures in /cache/game replaced.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        public static void TextureRestored() =>
            MessageBox.Show("Textures in /cache/game restored.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        public static void NoNewDistance() =>
            MessageBox.Show("Failed to find selected block. Try starting with disabled input.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
