class ReasonsForJoining {
  // TODO change string being used
  public enterReasonsForJoining(): this {
    cy.get('#ApplicationJoinTrustReason').type('Why does the school want to join this trust in particular?')

    cy.get('input[type=submit]').click()

    return this
  }
}

const reasonsForJoining = new ReasonsForJoining()

export default reasonsForJoining
