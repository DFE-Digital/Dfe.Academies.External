import BasePage from "../BasePage"
export default class A2BJAMTrustConsent extends BasePage {
    
static JAMTrustConsentElementsVisible() {
    cy.JAMTrustConsentElementsVisible()
}

static JAMTrustConsentFileUploadAndSubmit() {
    cy.JAMTrustConsentFileUpload()
    cy.get('input[type="submit"]').click()
}


}