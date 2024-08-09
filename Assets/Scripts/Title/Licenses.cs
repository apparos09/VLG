using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using util;

namespace VLG
{
    // The licenses for the game.
    public class Licenses : MonoBehaviour
    {
        // The credits interface.
        public AudioCreditsInterface creditsInterface;

        // The BGM audio credits for the game.
        public AudioCredits bgmCredits;

        // The BGM credits page.
        public int bgmCreditsIndex = 0;

        // The SFX audio credits for the game.
        public AudioCredits sfxCredits;
        
        // The sound effects credit page.
        public int sfxCreditsIndex = 0;

        // The font credits for the game.
        public AudioCredits fontsCredits;

        // The fonts credit page.
        public int fontsCreditsIndex = 0;

        [Header("UI")]

        // BGM Button
        public Button bgmButton;

        // SFX Button
        public Button sfxButton;

        // Fonts Button
        public Button fontsButton;

        // Start is called before the first frame update
        void Start()
        {
            // Loads all the credits.
            LoadBackgroundMusicCredits();
            LoadSoundEffectCredits();
            LoadFontCredits();

            // Sets this if it has not been set already.
            if(creditsInterface == null)
                creditsInterface = GetComponent<AudioCreditsInterface>();

            // Show the background credits.
            ShowBackgroundMusicCredits();
        }

        // Loads the BGM credits.
        private void LoadBackgroundMusicCredits()
        {
            AudioCredits.AudioCredit credit;

            // Title
            credit = new AudioCredits.AudioCredit();
            credit.title = "Middle Earth";
            credit.artist = "Jason Shaw";
            credit.collection = "SLOW";
            credit.source = "GameSounds.xyz, Audionautix.com";
            credit.link1 = "https://gamesounds.xyz/?dir=Audionautix/Soundtrack";
            credit.link2 = "https://audionautix.com/";
            credit.copyright =
                "\"Middle Earth\"\n" +
                "Music by Jason Shaw on Audionautix.com (https://audionautix.com)\n" +
                "Licensed under Creative Commons: By Attribution 4.0 License\n" +
                "http://creativecommons.org/licenses/by/4.0/";

            bgmCredits.audioCredits.Add(credit);

            // Area 1
            credit = new AudioCredits.AudioCredit();
            credit.title = "Magic Escape Room";
            credit.artist = "Kevin MacLeod";
            credit.collection = "Magic Escape Room";
            credit.source = "Incompetech.com";
            credit.link1 = "https://incompetech.com/music/royalty-free/music.html";
            credit.link2 = "";
            credit.copyright =
                "\"Magic Escape Room\" Kevin MacLeod (incompetech.com)\n" +
                "Licensed under Creative Commons: By Attribution 4.0 License\n" +
                "http://creativecommons.org/licenses/by/4.0/";

            bgmCredits.audioCredits.Add(credit);

            // Area 2
            credit = new AudioCredits.AudioCredit();
            credit.title = "Dream Fantasy";
            credit.artist = "Rafael Krux";
            credit.collection = "Fantasy - FREE MUSIC (CC-BY)";
            credit.source = "Orchestralis.net";
            credit.link1 = "https://music.orchestralis.net/track/28566231";
            credit.link2 = "";
            credit.copyright =
                "'Dream Fantasy' by Rafael Krux (orchestralis.net)\n" +
                "Creative Commons 4.0 License.\n" +
                "http://creativecommons.org/licenses/by/4.0/";

            bgmCredits.audioCredits.Add(credit);

            // Area 3
            credit = new AudioCredits.AudioCredit();
            credit.title = "Witch Waltz";
            credit.artist = "Kevin MacLeod";
            credit.collection = "FreePD Music";
            credit.source = "FreePD.com";
            credit.link1 = "https://freepd.com/misc.php";
            credit.link2 = "";
            credit.copyright =
                "\"Witch Waltz\"\n" +
                "Kevin MacLeod\n" +
                "Licensed under Creative Commons: By Attribution 4.0 License\n" +
                "http://creativecommons.org/licenses/by/4.0/";

            bgmCredits.audioCredits.Add(credit);

            // Area 4
            credit = new AudioCredits.AudioCredit();
            credit.title = "Eternal Story";
            credit.artist = "Rafael Krux";
            credit.collection = "Fantasy - FREE MUSIC (CC-BY)";
            credit.source = "Orchestralis.net";
            credit.link1 = "https://music.orchestralis.net/track/28566230";
            credit.link2 = "";
            credit.copyright =
                "'Eternal Story' by Rafael Krux (orchestralis.net)\n" +
                "Creative Commons 4.0 License.\n" +
                "http://creativecommons.org/licenses/by/4.0/";

            bgmCredits.audioCredits.Add(credit);

            // Area 5
            credit = new AudioCredits.AudioCredit();
            credit.title = "Fantasy Adventure";
            credit.artist = "Rafael Krux";
            credit.collection = "Fantasy - FREE MUSIC (CC-BY)";
            credit.source = "Orchestralis.net";
            credit.link1 = "https://music.orchestralis.net/track/28566238";
            credit.link2 = "";
            credit.copyright =
                "'Fantasy Adventure' by Rafael Krux (orchestralis.net)\n" +
                "Creative Commons 4.0 License.\n" +
                "http://creativecommons.org/licenses/by/4.0/";

            bgmCredits.audioCredits.Add(credit);

            // Area 6/Final Boss
            credit = new AudioCredits.AudioCredit();
            credit.title = "Nightmare Machine";
            credit.artist = "Kevin MacLeod";
            credit.collection = "Destruction Device";
            credit.source = "Incompetech.com";
            credit.link1 = "https://incompetech.com/music/royalty-free/music.html";
            credit.link2 = "";
            credit.copyright =
                "\"Nightmare Machine\" Kevin MacLeod (incompetech.com)\n" +
                "Licensed under Creative Commons: By Attribution 4.0 License\n" +
                "http://creativecommons.org/licenses/by/4.0/";

            bgmCredits.audioCredits.Add(credit);

            // Results
            credit = new AudioCredits.AudioCredit();
            credit.title = "Act Three";
            credit.artist = "Jason Shaw";
            credit.collection = "SLOW";
            credit.source = "GameSounds.xyz, Audionautix.com";
            credit.link1 = "https://gamesounds.xyz/?dir=Audionautix/Soundtrack";
            credit.link2 = "https://audionautix.com/";
            credit.copyright =
                "\"Act Three\"\n" +
                "Music by Jason Shaw on Audionautix.com (https://audionautix.com)\n" +
                "Licensed under Creative Commons: By Attribution 4.0 License\n" +
                "http://creativecommons.org/licenses/by/4.0/";

            bgmCredits.audioCredits.Add(credit);
        }

        // Loads the SFX credits.
        private void LoadSoundEffectCredits()
        {
            AudioCredits.AudioCredit credit = new AudioCredits.AudioCredit();

            // SOUND EFFECTS //
            // Floor Reset
            credit = new AudioCredits.AudioCredit();
            credit.title = "Text Message Alert 4";
            credit.artist = "Daniel Simon";
            credit.collection = "-";
            credit.source = "Soundbible.com";
            credit.link1 = "https://soundbible.com/2157-Text-Message-Alert-4.html";
            credit.link2 = "";
            credit.copyright =
                "\"Text Message Alert 4\"\n" +
                "Daniel Simon\n" +
                "Licensed under Creative Commons: By Attribution 3.0 License\n" +
                "https://creativecommons.org/licenses/by/3.0/";

            sfxCredits.audioCredits.Add(credit);

            // Player - Jump/Enemy - Jump
            credit = new AudioCredits.AudioCredit();
            credit.title = "Jump";
            credit.artist = "snottyboy";
            credit.collection = "-";
            credit.source = "Soundbible.com";
            credit.link1 = "https://soundbible.com/1343-Jump.html";
            credit.link2 = "";
            credit.copyright =
                "\"Jump\"\n" +
                "snottyboy\n" +
                "Licensed under Creative Commons: By Attribution 3.0 License\n" +
                "https://creativecommons.org/licenses/by/3.0/";

            sfxCredits.audioCredits.Add(credit);

            // Player - Attack
            credit = new AudioCredits.AudioCredit();
            credit.title = "Sword Swing";
            credit.artist = "Mike Koenig";
            credit.collection = "-";
            credit.source = "Soundbible.com";
            credit.link1 = "https://soundbible.com/1176-Sword-Swing.html";
            credit.link2 = "";
            credit.copyright =
                "\"Sword Swing\"\n" +
                "Mike Koenig\n" +
                "Licensed under Creative Commons: By Attribution 3.0 License\n" +
                "https://creativecommons.org/licenses/by/3.0/";

            sfxCredits.audioCredits.Add(credit);

            // Hazard Block - Hazard On/Off
            credit = new AudioCredits.AudioCredit();
            credit.title = "Knife Scrape Horror";
            credit.artist = "Mike Koenig";
            credit.collection = "";
            credit.source = "Soundbible.com";
            credit.link1 = "https://soundbible.com/405-Knife-Scrape-Horror.html";
            credit.link2 = "";
            credit.copyright =
                "\"Knife Scrape Horror\"\n" +
                "Mike Koenig\n" +
                "Licensed under Creative Commons: By Attribution 3.0 License\n" +
                "https://creativecommons.org/licenses/by/3.0/";

            sfxCredits.audioCredits.Add(credit);

            // Portal Block - On
            credit = new AudioCredits.AudioCredit();
            credit.title = "Laser Cannon";
            credit.artist = "Mike Koenig";
            credit.collection = "";
            credit.source = "Soundbible.com";
            credit.link1 = "https://soundbible.com/1771-Laser-Cannon.html";
            credit.link2 = "";
            credit.copyright =
                "\"Laser Cannon\"\n" +
                "Mike Koenig\n" +
                "Licensed under Creative Commons: By Attribution 3.0 License\n" +
                "https://creativecommons.org/licenses/by/3.0/";

            sfxCredits.audioCredits.Add(credit);

            // Portal Block - Off
            credit = new AudioCredits.AudioCredit();
            credit.title = "Power Failure";
            credit.artist = "Mike Koenig";
            credit.collection = "";
            credit.source = "Soundbible.com";
            credit.link1 = "https://soundbible.com/1610-Power-Failure.html";
            credit.link2 = "";
            credit.copyright =
                "\"Power Failure\"\n" +
                "Mike Koenig\n" +
                "Licensed under Creative Commons: By Attribution 3.0 License\n" +
                "https://creativecommons.org/licenses/by/3.0/";

            sfxCredits.audioCredits.Add(credit);

            // Button Block - Button Down/Butotn Up
            credit = new AudioCredits.AudioCredit();
            credit.title = "Button";
            credit.artist = "Mike Koenig";
            credit.collection = "";
            credit.source = "Soundbible.com";
            credit.link1 = "https://soundbible.com/772-Button.html";
            credit.link2 = "";
            credit.copyright =
                "\"Button\"\n" +
                "Mike Koenig\n" +
                "Licensed under Creative Commons: By Attribution 3.0 License\n" +
                "https://creativecommons.org/licenses/by/3.0/";

            sfxCredits.audioCredits.Add(credit);

            // Dragon - Fly Up/Fly Down
            credit = new AudioCredits.AudioCredit();
            credit.title = "Flapping Wings";
            credit.artist = "Mike Koenig";
            credit.collection = "";
            credit.source = "Soundbible.com";
            credit.link1 = "https://soundbible.com/627-Flapping-Wings.html";
            credit.link2 = "";
            credit.copyright =
                "\"Flapping Wings\"\n" +
                "Mike Koenig\n" +
                "Licensed under Creative Commons: By Attribution 3.0 License\n" +
                "https://creativecommons.org/licenses/by/3.0/";

            sfxCredits.audioCredits.Add(credit);

            // Thunder
            credit = new AudioCredits.AudioCredit();
            credit.title = "Perfect Thunder Storm";
            credit.artist = "Mike Koenig";
            credit.collection = "";
            credit.source = "Soundbible.com";
            credit.link1 = "https://soundbible.com/916-Perfect-Thunder-Storm.html";
            credit.link2 = "";
            credit.copyright =
                "\"Perfect Thunder Storm\"\n" +
                "Mike Koenig\n" +
                "Licensed under Creative Commons: By Attribution 3.0 License\n" +
                "https://creativecommons.org/licenses/by/3.0/";

            sfxCredits.audioCredits.Add(credit);

            // Lightning Strike
            credit = new AudioCredits.AudioCredit();
            credit.title = "Thunder Strike 1";
            credit.artist = "Mike Koenig";
            credit.collection = "";
            credit.source = "Soundbible.com";
            credit.link1 = "https://soundbible.com/2015-Thunder-Strike-1.html";
            credit.link2 = "";
            credit.copyright =
                "\"Thunder Strike 1\"\n" +
                "Mike Koenig\n" +
                "Licensed under Creative Commons: By Attribution 3.0 License\n" +
                "https://creativecommons.org/licenses/by/3.0/";

            sfxCredits.audioCredits.Add(credit);

            // Item - Get
            credit = new AudioCredits.AudioCredit();
            credit.title = "Twitch";
            credit.artist = "Mike Koenig";
            credit.collection = "";
            credit.source = "Soundbible.com";
            credit.link1 = "https://soundbible.com/2015-Thunder-Strike-1.html";
            credit.link2 = "";
            credit.copyright =
                "\"Twitch\"\n" +
                "Mike Koenig\n" +
                "Licensed under Creative Commons: By Attribution 3.0 License\n" +
                "https://creativecommons.org/licenses/by/3.0/";

            sfxCredits.audioCredits.Add(credit);



            // JINGLES //
            // Floor Clear/Goal Reached, Boss Clear/Boss Goal Reached, Game Clear/Game End
            credit = new AudioCredits.AudioCredit();
            credit.title = "Big Intro";
            credit.artist = "Jason Shaw";
            credit.collection = "SLOW";
            credit.source = "GameSounds.xyz, Audionautix.com";
            credit.link1 = "https://gamesounds.xyz/?dir=Audionautix/Soundtrack";
            credit.link2 = "https://audionautix.com/";
            credit.copyright =
                "\"Big Intro\"\n" +
                "Music by Jason Shaw on Audionautix.com (https://audionautix.com)\n" +
                "Licensed under Creative Commons: By Attribution 4.0 License\n" +
                "http://creativecommons.org/licenses/by/4.0/";

            sfxCredits.audioCredits.Add(credit);
        }

        // Loads the font credits.
        private void LoadFontCredits()
        {
            AudioCredits.AudioCredit credit = new AudioCredits.AudioCredit();

            // Loads the font credit.
            credit.title = "Cinzel";
            credit.artist = "Natanael Gama";
            credit.collection = "";
            credit.source = "1001Fonts.com";
            credit.link1 = "https://www.1001fonts.com/cinzel-font.html";
            credit.link2 = "";
            credit.copyright = 
                "\"Cinzel\"\n" +
                "Natanael Gama\n" + 
                "Licensed under SIL Open Font Licence: By Attribution Version 1.1\n" + 
                "https://openfontlicense.org/open-font-license-official-text/";

            // Adds the font credit.
            fontsCredits.audioCredits.Add(credit);
        }

        // Enables all the credit buttons.
        public void EnableAllCreditButtons()
        {
            // Disables all the credit buttons.
            bgmButton.interactable = true;
            sfxButton.interactable = true;
            fontsButton.interactable = true;
        }

        // Disables all the credit buttons.
        public void DisableAllCreditButtons()
        {
            // Disables all the credit buttons.
            bgmButton.interactable = false;
            sfxButton.interactable = false;
            fontsButton.interactable = false;
        }

        // Shows the BGM credits.
        public void ShowBackgroundMusicCredits()
        {
            // Saves the current credit index, switches over, and then sets the new credit index.
            bgmCreditsIndex = creditsInterface.GetCurrentCreditIndex();
            creditsInterface.audioCredits = bgmCredits;
            creditsInterface.SetCreditIndex(bgmCreditsIndex);

            // Change button settings.
            EnableAllCreditButtons();
            bgmButton.interactable = false;
            sfxButton.Select();
        }

        // Shows the SFX credits.
        public void ShowSoundEffectCredits()
        {
            // Saves the current credit index, switches over, and then sets the new credit index.
            sfxCreditsIndex = creditsInterface.GetCurrentCreditIndex();
            creditsInterface.audioCredits = sfxCredits;
            creditsInterface.SetCreditIndex(sfxCreditsIndex);

            // Change button settings.
            EnableAllCreditButtons();
            sfxButton.interactable = false;
            bgmButton.Select();

        }

        // Shows the font credits.
        public void ShowFontCredits()
        {
            // Saves the current credit index, switches over, and then sets the new credit index.
            fontsCreditsIndex = creditsInterface.GetCurrentCreditIndex();
            creditsInterface.audioCredits = fontsCredits;
            creditsInterface.SetCreditIndex(fontsCreditsIndex);

            // Change button settings.
            EnableAllCreditButtons();
            fontsButton.interactable = false;
            sfxButton.Select();
        }
    }
}