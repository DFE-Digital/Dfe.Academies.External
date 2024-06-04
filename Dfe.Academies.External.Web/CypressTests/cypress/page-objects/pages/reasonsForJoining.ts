class ReasonsForJoining {
  public reasonsForJoiningElementsVisible(): this {
    cy.reasonsForJoiningElementsVisible()

    return this
  }

  public reasonsForJoiningInputAndSubmit(): this {
    cy.reasonsForJoiningInput()
    cy.submitReasonsForJoining()

    return this
  }
}

const reasonsForJoining = new ReasonsForJoining()

export default reasonsForJoining
