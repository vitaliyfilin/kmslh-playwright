# ATDD Test Cases for KMSLH

## Test Case 1: Validate “Book a Demo” Link Navigation

**Steps**

1. Open https://kmslh.com/ (homepage)
2. Click on the **Book a Demo** button
3. Verify that the site redirects to the Demo Request form page
4. Validate the demo request form has fields: Name, Email, Company, Phone (all interactable)
   **Note:** DO NOT click the final submit on the demo form.

## Test Case 2: Validate Default State of Accessibility Toggles

**Steps**

1. Open https://kmslh.com/
2. Click the accessibility widget
3. Observe defaults for toggles: Bigger Text, Contrast Toggle, Dyslexia-friendly Font, Highlight Links
4. Pick one toggle (e.g., Bigger Text). Toggle it on and validate visual change. Toggle it off and validate UI returns
   to original state.
