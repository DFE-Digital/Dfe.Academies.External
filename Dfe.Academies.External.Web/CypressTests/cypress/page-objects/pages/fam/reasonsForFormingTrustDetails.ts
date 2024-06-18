// TODO refactor
const FAM_WHY_FORM_TRUST = 'Why are the schools forming the trust? This could include the expected benefits or existing links you are building on.'
const FAM_WHAT_VISION = 'What vision and aspiration have the schools agreed for the trust?'
const FAM_AREAS_AND_COMMUNITIES = 'What geographical areas and communities will the trust service?'
const FAM_FREEDOM = 'How will the schools make the most of the freedoms that academies have?'
const FAM_HOW_IMPROVE_TEACHING = 'How will the schools work together to improve teaching and learning in the trust and potentially support other schools beyond the trust?'

class ReasonsForFormingTrustDetails {
  public FAMFillReasonsForFormingTrustAndSubmit(): this {
    cy.get('#ReasonForming').click()
    cy.get('#ReasonForming').type(FAM_WHY_FORM_TRUST)

    cy.get('#ReasonVision').click()
    cy.get('#ReasonVision').type(FAM_WHAT_VISION)

    cy.get('#GeoAreas').click()
    cy.get('#GeoAreas').type(FAM_AREAS_AND_COMMUNITIES)

    cy.get('#Freedom').click()
    cy.get('#Freedom').type(FAM_FREEDOM)

    cy.get('#ImproveTeaching').click()
    cy.get('#ImproveTeaching').type(FAM_HOW_IMPROVE_TEACHING)

    cy.get('input[type=submit]').should('be.visible').contains('Save and continue').click()

    return this
  }
}

const reasonsForFormingTrustDetails = new ReasonsForFormingTrustDetails()

export default reasonsForFormingTrustDetails
