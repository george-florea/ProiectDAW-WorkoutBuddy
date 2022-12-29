function AuthHeader() {
  const token = sessionStorage.getItem("token");

  if (token) {
    const transfToken = token.substring(0, token.length);
    return `Bearer ${token}`;
  } else {
    return {};
  }
}
export default AuthHeader;
