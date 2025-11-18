<script lang="ts">
  // You can add logic here if needed like shared state or imports or functions.
</script>

<header class="site-header">    
    <nav class="nav">
        <a data-sveltekit-prefetch href="/" class="home-btn">Hjem</a>
        <!-- add other global links here, e.g.:
    <a href="/foods">Foods</a>
    -->
      <a data-sveltekit-prefetch href="/foods" class="home-btn">Matvaretabellen</a>
        <a data-sveltekit-prefetch href="/foods/UserInput_FavoriteFoods" class="home-btn">Favorites</a>
    </nav>
</header>

<main class="content">
    <slot />
</main>

<style>
  .site-header {
    background: #f8fafc;
    border-bottom: 1px solid #e6e6e6;
    padding: 0.5rem 1rem;
    position: sticky;
    top: 0;
    z-index: 10;
  }
  .nav {
    display: flex;
    gap: 1rem;
    align-items: center;
  }
  .home-btn {
    display: inline-block;
    padding: 0.4rem 0.8rem;
    background: #2a7ae2;
    color: white;
    border-radius: 6px;
    text-decoration: none;
    font-weight: 600;
  }
  .home-btn:hover {
    background: #1f63c6;
  }
  .content {
    padding: 1rem;
  }
</style>