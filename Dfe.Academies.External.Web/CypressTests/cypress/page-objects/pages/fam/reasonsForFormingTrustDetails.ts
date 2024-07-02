class ReasonsForFormingTrustDetails {
  public enterReasonsForFormingTrust(): this {
    cy.get('#ReasonForming').click()
    cy.get('#ReasonForming').type('Why are the schools forming the trust?')

    cy.get('#ReasonVision').click()
    cy.get('#ReasonVision').type('What vision and aspiration have the schools agreed for the trust?')

    cy.get('#GeoAreas').click()
    cy.get('#GeoAreas').type('What geographical areas and communities will the trust service?')

    cy.get('#Freedom').click()
    cy.get('#Freedom').type('How will the schools make the most of the freedoms that academies have?')

    cy.get('#ImproveTeaching').click()
    cy.get('#ImproveTeaching').type('How will the schools work together to improve teaching and learning in the trust and potentially support other schools beyond the trust?')

    cy.get('input[type=submit]').should('be.visible').contains('Save and continue').click()

    return this
  }
}

const reasonsForFormingTrustDetails = new ReasonsForFormingTrustDetails()

export default reasonsForFormingTrustDetails
