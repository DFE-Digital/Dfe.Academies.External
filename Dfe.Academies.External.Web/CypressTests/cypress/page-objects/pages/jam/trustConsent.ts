class TrustConsent {
  public JAMTrustConsentElementsVisible(): this {
    cy.JAMTrustConsentElementsVisible()

    return this
  }

  public JAMTrustConsentFileUploadAndSubmit(): this {
    cy.JAMTrustConsentFileUpload()
    cy.get('input[type=submit]').click()

    return this
  }
}

const trustConsent = new TrustConsent()

export default trustConsent
