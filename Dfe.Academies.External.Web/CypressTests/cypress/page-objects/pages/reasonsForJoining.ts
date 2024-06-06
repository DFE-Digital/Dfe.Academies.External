class ReasonsForJoining {
  public reasonsForJoiningInputAndSubmit(): this {
    const reasonsForJoining = 'Why does the school want to join this trust in particular? Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.'
    cy.get('#ApplicationJoinTrustReason').type(reasonsForJoining)

    cy.get('input[type=submit]').click()

    return this
  }
}

const reasonsForJoining = new ReasonsForJoining()

export default reasonsForJoining
