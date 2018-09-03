using UnityEngine;

namespace JohnsonCodeHK.UIControllerExamples.Popup {

	public class UIControllerExample : UIController {

        // Base public Functions:
        // - void Show, Hide ()
        // - void Show, Hide (UnityAction)

        // Base virtual Functions:
        // - public void Show, Hide ()
        // - protected void OnShow, OnHide ()

        // Base public Pars:
        // - OnHideAction onHideAction
        // - UnityEvent onShow, onHide
        // - bool isShow, isPlaying
        // - Animator animator

        // OnShow, OnHide evect stpes:
        // Override > Listener > Callback

        public GameObject Title;

        public GameObject PlayButton;
        
		// Listeners
		void Awake () {
			this.onShow.AddListener (() => {
				print (this.name + ": OnShow (AddListener in Awake)");
			});
			this.onHide.AddListener (() => {
				print (this.name + ": OnHide (AddListener in Awake)");
			});
		}

		// Override
		public override void Show () {
			//print (this.name + ": Show (Override)");
			base.Show ();
            Title.SetActive(false);
            PlayButton.SetActive(false);
		}
		public override void Hide () {
			//print (this.name + ": Hide (Override)");
			base.Hide ();
            Title.SetActive(true);
            PlayButton.SetActive(true);
        }
		protected override void OnShow () {
			//print (this.name + ": OnShow (Override)");
			base.OnShow ();
		}
		protected override void OnHide () {
			//print (this.name + ": OnHide (Override)");
			base.OnHide ();
		}

		// Callback
		public void Play () {
			if (!this.isShow) {
				// this.Show (); // No Callback
				this.Show (() => { // Callback
					print (this.name + ": OnShow (Callback)");
				});
			}
			else {
				// this.Hide (); // No Callback
				this.Hide (() => { // Callback
					print (this.name + ": OnHide (Callback)");
				});
			}
		}
	}
}
