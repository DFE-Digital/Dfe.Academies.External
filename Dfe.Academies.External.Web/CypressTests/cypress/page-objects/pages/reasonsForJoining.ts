class ReasonsForJoining {
  public enterReasonsForJoining(): this {
    cy.get('#ApplicationJoinTrustReason').type('Reason to join this trust')

    cy.get('input[type=submit]').click()

    return this
  }
}

const reasonsForJoining = new ReasonsForJoining()

export default reasonsForJoining
