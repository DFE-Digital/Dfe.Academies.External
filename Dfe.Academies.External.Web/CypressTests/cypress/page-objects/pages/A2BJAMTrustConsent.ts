import BasePage from "../BasePage"
export default class A2BJAMTrustConsent extends BasePage {
    
static JAMTrustConsentElementsVisible() {
    cy.JAMTrustConsentElementsVisible()
}

static JAMTrustConsentFileUpload() {
    cy.JAMTrustConsentFileUpload()
}

static JAMTrustConsentSubmit() {
    cy.get('input[type="submit"]').click()
}


}