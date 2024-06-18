// TODO refactor
const FAM_WHY_FORM_TRUST = 'Why are the schools forming the trust? This could include the expected benefits or existing links you are building on.'
const FAM_WHAT_VISION = 'What vision and aspiration have the schools agreed for the trust?'
const FAM_AREAS_AND_COMMUNITIES = 'What geographical areas and communities will the trust service?'
const FAM_FREEDOM = 'How will the schools make the most of the freedoms that academies have?'
const FAM_HOW_IMPROVE_TEACHING = 'How will the schools work together to improve teaching and learning in the trust and potentially support other schools beyond the trust?'

class ReasonsForFormingTrustSummary {
  public selectStartSection(): this {
    cy.contains('Start section').click()

    return this
  }

  // TODO improve selectors
  public FAMReasonsForFormingTrustSummaryCompleteElementsVisibleAndSubmit(): this {
    cy.get('p').eq(3).contains(FAM_WHY_FORM_TRUST)
    cy.get('p').eq(5).contains(FAM_WHAT_VISION)
    cy.get('p').eq(7).contains(FAM_AREAS_AND_COMMUNITIES)
    cy.get('p').eq(9).contains(FAM_FREEDOM)
    cy.get('p').eq(11).contains(FAM_HOW_IMPROVE_TEACHING)

    cy.get('.govuk-button').should('be.visible').contains('Save and return to your application').click()

    return this
  }
}

const reasonsForFormingTrustSummary = new ReasonsForFormingTrustSummary()

export default reasonsForFormingTrustSummary
